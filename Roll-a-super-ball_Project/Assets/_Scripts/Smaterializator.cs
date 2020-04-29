using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smaterializator : MonoBehaviour
{
    [Range(0,1)]
    public float visibility;
    public float fadeTime = 1f;

    private Material mat;
    private string visibilityName = "Vector1_6566B52F";

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        visibility = 1;
    }
    void Update()
    {
        float interp = (Time.deltaTime) / fadeTime * 2;
        float vis = Mathf.Lerp( mat.GetFloat(visibilityName) , visibility, interp);
        mat.SetFloat(visibilityName, vis);
    }

    public void FadeIn()
    {
        visibility = 1;
    }

    public void FadeOut()
    {
        visibility = 0;
    }
}
