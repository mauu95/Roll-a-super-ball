using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public PowerUpIcon[] list;

    public void Clear()
    {
        InventorySlot[] slots = GetComponentsInChildren<InventorySlot>();
        foreach (InventorySlot slot in slots)
            Destroy(slot.gameObject);
    }

    public void Add(PowerUp item, string text)
    {
        GameObject temp = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, transform);
        InventorySlot slot = temp.GetComponent<InventorySlot>();
        
        slot.SetText(text);
        slot.SetPowerUp(item);

        foreach (PowerUpIcon pow in list)
            if (item.id == pow.name)
                slot.SetIcon(pow.icon);

    }

    public void Highlight(int i)
    {
        foreach(InventorySlot slot in GetComponentsInChildren<InventorySlot>())
            slot.DeHightlight();

        GetComponentsInChildren<InventorySlot>()[i].Highlight();
    }

    [Serializable]
    public struct PowerUpIcon
    {
        public string name;
        public Sprite icon;
    }

    public void ChangeSlotsPosWithPrevious(int pos)
    {
        InventorySlot[] slots = GetComponentsInChildren<InventorySlot>();
        slots[pos].transform.SetSiblingIndex(pos - 1);

        foreach (InventorySlot slot in slots)
            slot.SetText((slot.transform.GetSiblingIndex() + 1).ToString());
    }
}
