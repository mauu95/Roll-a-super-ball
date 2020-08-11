﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int level;
    public string description;
    public Sprite locked;
    public Sprite unlocked;

    private void Start()
    {
        if (GameManager.instance.getPickUpsValue(level - 1) >= (level - 1) * 7)
            GameManager.instance.Unlock(level);

        int npick = GameManager.instance.getPickUpsValue(level);
        int ntotpick;

        if (level == 0)
            ntotpick = 15;
        else
            ntotpick = level * 10;

        description = "Level " + level + "\n" + npick + "/" + ntotpick; 

        if (IsUnlocked())
            GetComponent<Image>().sprite = unlocked;
        else
            GetComponent<Image>().sprite = locked;
    }

    public bool IsUnlocked()
    {
        return GameManager.instance.getPickUpsValue(level) >= 0;
    }

    
}
