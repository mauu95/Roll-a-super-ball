using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTitile : MonoBehaviour
{
    void Start()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        int level = GameManager.instance.getCurrentLevel();


        if (level >= GameManager.LEVEL_COUNT - 1)
            text.text = "SPECIAL LEVEL";
        else
            text.text = "Level " + level;

        Animator anim = GetComponent<Animator>();
        if (anim)
            anim.Play("TitleFadeOut");
    }
}
