using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState: MonoBehaviour
{
    public enum LevelState
    {
        LOCKED,
        UNLOCKED,
        COMPLETED
    }

    private const string PICKUP_LEVEL_KEY = "PickUpLevel";
    private int nlevels;

    public GameState(int n)
    {
        nlevels = n;
        if(getLevelState(0) == LevelState.LOCKED)
            PlayerPrefs.SetInt(PICKUP_LEVEL_KEY + "0", 0);
    }

    public void UpdateLevel(int level, int value)
    {
        int old = PlayerPrefs.GetInt(PICKUP_LEVEL_KEY + level, -1);
        if (old < value)
        {
            PlayerPrefs.SetInt(PICKUP_LEVEL_KEY + level, value);
            if(value >= level * 7)
                PlayerPrefs.SetInt(PICKUP_LEVEL_KEY + level + 1, 0);
        }
            
    }

    public void reset()
    {
        for (int i = 0; i < nlevels; i++)
            PlayerPrefs.SetInt(PICKUP_LEVEL_KEY + i, -1);

        PlayerPrefs.SetInt(PICKUP_LEVEL_KEY + "0", 0);
    }

    public LevelState getLevelState(int n)
    {
        int npicks = PlayerPrefs.GetInt(PICKUP_LEVEL_KEY + n, -1);

        if (npicks == -1)
            return LevelState.LOCKED;
        else
        {
            return LevelState.UNLOCKED;
        }
            
    }

}