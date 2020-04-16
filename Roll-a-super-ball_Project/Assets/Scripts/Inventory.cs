using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<PowerUp> powerUps = new List<PowerUp>();
    public int pickUps;
    public InventoryUI inventoryUI;
    public PowerUpIcons icons;

    public Text countText;
    public Text winText;

    private int current;

    private void Start()
    {
        pickUps = 0;
        countText.text = "COUNT: " + pickUps.ToString();
    }

    private void Update()
    {
        for (int i = 1; i < 10; i++)
            if (Input.GetKeyDown(i.ToString()))
                ActivatePowerUp(i - 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            pickUps++;
            countText.text = "COUNT: " + pickUps.ToString();

            if (pickUps == 4)
                winText.gameObject.SetActive(true);
        }
    }


    public bool AddPowerUp(PowerUp item)
    {
        powerUps.Add(item);
        UpdateUI();
        return true;
    }

    public void UpdateUI()
    {
        inventoryUI.Clear();
        for(int i = 0; i < powerUps.Count; i++)
            inventoryUI.Add(powerUps[i], (i+1).ToString());
        ActivatePowerUp(current);
    }

    public void DeactivatePowerUp()
    {
        foreach(PowerUp item in powerUps)
            item.enabled = false;
    }

    public void ActivatePowerUp(int i)
    {
        if (i < 0 || i > powerUps.Count -1 )
            return;
        DeactivatePowerUp();
        inventoryUI.Highlight(i);
        powerUps[i].enabled = true;
        current = i;
    }

}
