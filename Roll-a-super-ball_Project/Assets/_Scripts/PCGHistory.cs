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

    public int[] SearchPattern(string pattern)
    {
        List<int> result = new List<int>();
        string sHist = ToString();
        sHist = sHist.Replace(" ", "");

        Regex rx = new Regex(pattern);
        MatchCollection matches = rx.Matches(sHist);

        foreach(Match match in matches)
        {
            GroupCollection groups = match.Groups;
            result.Add(groups[0].Index);
        }

        return result.ToArray();
    }

    public override string ToString()
    {
        string result = "";

        foreach(PCGHistoryStep step in hist)
            result += " " + step.action;

        return result;
    }
}
