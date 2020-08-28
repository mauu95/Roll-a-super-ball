using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DifficultySetter : MonoBehaviour
{
    public Slider sizeSlider;
    public Slider nFloorSlider;
    public Slider nEnemySlider;

    private int sliderSizeInit;
    private int sliderNFloorInit;
    private int sliderNEnemyInit;

    private void Start()
    {
        GameManager.instance.CursorOn();

        sliderSizeInit = PlayerPrefs.GetInt("sliderSizeInit");
        sliderNFloorInit = PlayerPrefs.GetInt("sliderNFloorInit");
        sliderNEnemyInit = PlayerPrefs.GetInt("sliderNEnemyInit");

        if (sliderSizeInit == 0)
            sliderSizeInit = (int)sizeSlider.minValue;
        if (sliderNFloorInit == 0)
            sliderNFloorInit = (int)nFloorSlider.minValue;

        sizeSlider.value = sliderSizeInit;
        nFloorSlider.value = sliderNFloorInit;
        nEnemySlider.value = sliderNEnemyInit;
    }

    public void StartLevel()
    {
        int dim = (int)sizeSlider.value;
        int nFloor = (int)nFloorSlider.value;
        int nAgents = (int)nEnemySlider.value;

        PlayerPrefs.SetInt("sliderSizeInit", dim);
        PlayerPrefs.SetInt("sliderNFloorInit", nFloor);
        PlayerPrefs.SetInt("sliderNEnemyInit", nAgents);

        GameManager.instance.LoadSpecialLevel();
    }
}
