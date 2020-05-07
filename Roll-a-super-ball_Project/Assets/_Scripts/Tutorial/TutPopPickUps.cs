using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TutPopPickUps : TutorialPopUp
{
    public GameObject pickUpPrefab;
    public Inventory player;
    public GameObject ScoreUI;

    private void Start()
    {
        ScoreUI.SetActive(true);
        Instantiate(pickUpPrefab, new Vector3(5f, 0.5f, 0f), Quaternion.identity);
        Instantiate(pickUpPrefab, new Vector3(-5f, 0.5f, 0f), Quaternion.identity);
        Instantiate(pickUpPrefab, new Vector3(0f, 0.5f, 5f), Quaternion.identity);
        Instantiate(pickUpPrefab, new Vector3(0f, 0.5f, -5f), Quaternion.identity);
    }

    private void Update()
    {
        if (player.pickUps == 4)
            End();
    }
}
