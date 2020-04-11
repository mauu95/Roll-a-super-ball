using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;

    public void Clear()
    {
        InventorySlot[] slots = GetComponentsInChildren<InventorySlot>();
        foreach (InventorySlot slot in slots)
        {
            Destroy(slot.gameObject);
        }
    }

    public void Add(PowerUp item, string text)
    {
        GameObject temp = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, transform);
        temp.GetComponent<InventorySlot>().SetText(text);
    }

    public void Highlight(int i)
    {
        foreach(InventorySlot slot in GetComponentsInChildren<InventorySlot>())
        {
            slot.DeHightlight();
        }
        GetComponentsInChildren<InventorySlot>()[i].Highlight();
    }
}
