﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class TutPopPickUps : TutorialPopUp
{
    public GameObject pickUpPrefab;
    public GameObject ScoreUI;
    public TextMeshProUGUI pickUpCount;

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
        Inventory pinv = player.GetComponent<Inventory>();
        pickUpCount.text = pinv.pickUps + "/" + 4;
        if (pinv.pickUps == 4)
            End();
    }
}
