using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public List<PowerUp> powerUps = new List<PowerUp>();
    public int pickUps;
    public InventoryUI inventoryUI;
    public PowerUpIcons icons;

    public TextMeshProUGUI countText;
    public Text winText;

    private int current = -1;

    private void Start() {
        pickUps = 0;
        countText.text = pickUps.ToString() + "/" + GameManager.instance.nPickUp.ToString();
    }

    private void Update() {
        for (int i = 1; i < 10; i++)
            if (Input.GetKeyDown(i.ToString()))
                ActivatePowerUp(i - 1);

        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
             ActivatePowerUp(current + (int)scroll);


        if (Input.GetMouseButtonDown(1))
        {
            if (current - 1 < 0)
                return;
            PowerUp temp = powerUps[current];
            powerUps[current] = powerUps[current - 1];
            powerUps[current - 1] = temp;

            inventoryUI.ChangeSlotsPosWithPrevious(current);
            current--;
        }


    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            pickUps++;
            countText.text = pickUps.ToString() + "/" + GameManager.instance.nPickUp.ToString();
            countText.GetComponent<Animator>().Play("BigToSmall");

            if (pickUps == GameManager.instance.nPickUp)
                winText.gameObject.SetActive(true);
        }
    }


    public bool AddPowerUp(PowerUp item) {
        powerUps.Add(item);
        UpdateUIAfterAdd();
        return true;
    }
        
    public void UpdateUIAfterAdd() {
        inventoryUI.Add(powerUps[powerUps.Count - 1], (powerUps.Count).ToString());
        if(current>=0)
            inventoryUI.Highlight(current);
    }

    public void DeactivatePowerUp() {
        foreach (PowerUp item in powerUps)
        {
            item.ReturnToNormal();
            item.enabled = false;
        }
    }

    public void ActivatePowerUp(int i) {
        if (i < 0 || i > powerUps.Count - 1)
            return;
        DeactivatePowerUp();
        inventoryUI.Highlight(i);
        powerUps[i].enabled = true;
        current = i;
    }

}
