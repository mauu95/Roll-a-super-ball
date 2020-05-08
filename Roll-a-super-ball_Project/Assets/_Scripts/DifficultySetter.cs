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

    public void StartLevel()
    {
        mapCreator.Dimension = (int) sizeSlider.value;

        mapCreator.gameObject.SetActive(true);
        player.SetActive(true);

        GameManager.instance.CursorOff();

        gameObject.SetActive(false);
    }
}
