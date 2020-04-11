using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int space = 9;
    public List<PowerUp> items = new List<PowerUp>();
    public InventoryUI inventoryUI;

    public bool Add(PowerUp item)
    {
        items.Add(item);
        UpdateUI();
        return true;
    }

    public void UpdateUI()
    {
        inventoryUI.Clear();
        foreach(PowerUp item in items)
        {
            inventoryUI.Add(item);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            gameObject.AddComponent<PowerUpDash>();
            Add(GetComponent<PowerUpDash>());
        }

    }
}
