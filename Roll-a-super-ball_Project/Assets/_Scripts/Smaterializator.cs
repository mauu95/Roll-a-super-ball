using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smaterializator : MonoBehaviour
{
    [Range(0,1)]
    public float visibility;
    public float fadeTime = 1f;
    public bool isFading;

    private Material mat;
    private string visibilityName = "Vector1_6566B52F";

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;

        if (!mat.HasProperty(visibilityName))
            enabled = false;

        FadeIn();
    }
    void Update()
    {
        float interp = (Time.deltaTime) / fadeTime * 2;
        float vis = Mathf.Lerp( mat.GetFloat(visibilityName) , visibility, interp);
        mat.SetFloat(visibilityName, vis);


        isFading = Mathf.Abs(visibility - vis) > 0.1;
    }

    public void FadeIn()
    {
        visibility = 1;
        isFading = true;
    }

    public void FadeOut()
    {
        AudioManager.instance.Play("Smaterialization");
        visibility = 0;
        isFading = true;
    }
}
