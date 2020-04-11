using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public void SetText(string newtext)
    {
        GetComponentInChildren<Text>().text = newtext;
    }

    public void SetIcon(Sprite icon)
    {
        GetComponent<Image>().sprite = icon;
    }

    public void Highlight()
    {
        GetComponent<Image>().color = Color.green;
    }

    public void DeHightlight()
    {
        GetComponent<Image>().color = Color.white;
    }
}
