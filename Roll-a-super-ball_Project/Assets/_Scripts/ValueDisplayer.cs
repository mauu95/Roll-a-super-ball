using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValueDisplayer : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = slider.value.ToString();
    }

    public void UpdateText()
    {
        GetComponent<TextMeshProUGUI>().text = slider.value.ToString();
    }
}
