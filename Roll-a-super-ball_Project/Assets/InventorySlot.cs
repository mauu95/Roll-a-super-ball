using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public void setIcon(Sprite icon)
    {
        GetComponent<Image>().sprite = icon;
    }
}
