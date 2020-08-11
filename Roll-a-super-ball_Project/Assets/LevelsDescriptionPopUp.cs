using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelsDescriptionPopUp : MonoBehaviour
{
    public Vector3 offset;
    public TextMeshProUGUI text;

    public void showOn(LevelButton other) {
        if (other.IsUnlocked())
            GetComponent<Image>().color = new Color32(53, 139, 226, 140);
        else
            GetComponent<Image>().color = new Color32(217, 29, 29, 140);


        if (other.IsUnlocked())
            text.SetText(other.description);
        else
            text.SetText("You need to complete the previous level");


        transform.position = other.transform.position + offset;
        gameObject.SetActive(true);
        
    }
}
