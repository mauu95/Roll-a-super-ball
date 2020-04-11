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
        for(int i = 0; i < items.Count; i++)
        {
            inventoryUI.Add(items[i], (i+1).ToString());
        }
    }

    public void DeactivatePowerUp()
    {
        foreach(PowerUp item in items)
        {
            item.enabled = false;
        }
    }

    public void activatePowerUp(int i)
    {
        DeactivatePowerUp();
        inventoryUI.Highlight(i);
        items[i].enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activatePowerUp(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activatePowerUp(1);
        }
    }

}
