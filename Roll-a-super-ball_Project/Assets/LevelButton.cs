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
    public Sprite completed;

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

        if (level == 6)
            description = "SPECIAL";

        if(IsCompleted())
            GetComponent<Image>().sprite = completed;
        else if (IsUnlocked())
            GetComponent<Image>().sprite = unlocked;
        else
            GetComponent<Image>().sprite = locked;
    }

    public bool IsUnlocked()
    {
        return GameManager.instance.getPickUpsValue(level) >= 0;
    }

    public bool IsCompleted()
    {
        int npicks = GameManager.instance.getPickUpsValue(level);

        if (level == 0)
            return npicks == 15;
        else
            return npicks == level * 10;
    }

    
}
