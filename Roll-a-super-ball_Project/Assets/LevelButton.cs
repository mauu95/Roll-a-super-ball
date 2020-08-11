using System.Collections;
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
