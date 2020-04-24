using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Text.RegularExpressions;

public class PCGHistory : MonoBehaviour
{
    public List<PCGHistoryStep> hist;

    public PCGHistory()
    {
        hist = new List<PCGHistoryStep>();
    }

    public void Add(int action, GameObject obj = null)
    {
        hist.Add(new PCGHistoryStep(action, obj));
    }

    public int[] ToArray()
    {
        int[] result = new int[hist.Count];

        for(int i = 0; i<hist.Count; i++)
            result[i] = hist[i].action;

        return result;
    }

    public SearchPatternResult[] SearchBrigde(string pattern)
    {
        List<SearchPatternResult> result = new List<SearchPatternResult>();
        string sHist = ToStringWithNoSpaces();
        sHist = sHist.Replace(" ", "");

        Regex rx = new Regex(pattern);
        MatchCollection matches = rx.Matches(sHist);

        foreach(Match match in matches)
        {
            SearchPatternResult temp = new SearchPatternResult();
            temp.index = match.Index;
            temp.match = match.Value;
            temp.offset = match.Value.IndexOf("2");
            temp.indexOfBridge = temp.index + temp.offset;
            result.Add(temp);
        }

        return result.ToArray();
    }

    public PCGHistoryStep GetElement(int index)
    {
        return hist[index];
    }

    public string ToStringWithNoSpaces()
    {
        string result = "";

        foreach(PCGHistoryStep step in hist)
            result += step.action;

        return result;
    }

    public override string ToString()
    {
        string result = "";

        for (int i =0; i < hist.Count; i++)
        {
            result += " " + i + ":" + hist[i].action;
        }

        return result;
    }

    public struct SearchPatternResult
    {
        public int index;
        public string match;
        public int offset;
        public int indexOfBridge;
    }
}
