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
        var image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
    }

    public void DeHightlight()
    {
        var image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.3f);
    }
}
