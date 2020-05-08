using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultySetter : MonoBehaviour
{
    public PCGMap mapCreator;
    public GameObject player;

    public Slider sizeSlider;
    public Slider nFloorSlider;
    public Slider nEnemySlider;

    private void Start()
    {
        GameManager.instance.CursorOn();
    }

    public void StartLevel()
    {
        int dim = (int)sizeSlider.value;
        int nFloor = (int)nFloorSlider.value;

        mapCreator.Dimension = dim;
        mapCreator.nFloor = nFloor;
        mapCreator.nAgents = (int)nEnemySlider.value;

        mapCreator.nPickUps = (int)(dim * nFloor / 10);

        mapCreator.gameObject.SetActive(true);
        player.SetActive(true);

        GameManager.instance.CursorOff();

        gameObject.SetActive(false);
    }
}
