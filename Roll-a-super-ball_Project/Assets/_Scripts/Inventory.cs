using System;
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
    public PCGMap map;
    public LevelCompleteBox winBox;

    private int current = -1;
    private int totalPickUps;

    private void Start() {
        pickUps = 0;

        int currentLevel = GameManager.instance.getCurrentLevel();

        if (currentLevel == 0)
            totalPickUps = 15;
        else
            totalPickUps = GameManager.instance.getCurrentLevel() * 10;
        countText.text = pickUps.ToString() + "/" + totalPickUps;
        winBox = FindObjectOfType<LevelCompleteBox>();
    }

    private void Update() {
        if (GameManager.instance.IsPause)
            return;

        for (int i = 1; i < 10; i++)
            if (Input.GetKeyDown(i.ToString()))
                ActivatePowerUp(i - 1);

        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
            ActivatePowerUp(current + (int)scroll);


        if (Input.GetMouseButtonDown(0)) {
            if (current - 1 < 0)
                return;
            PowerUp temp = powerUps[current];
            powerUps[current] = powerUps[current - 1];
            powerUps[current - 1] = temp;

            inventoryUI.ChangeSlotsPosWithPrevious(current);
            current--;
        }
        if (Input.GetMouseButtonDown(1)) {
            if (current + 1 > powerUps.Count - 1)
                return;
            PowerUp temp = powerUps[current];
            powerUps[current] = powerUps[current + 1];
            powerUps[current + 1] = temp;

            inventoryUI.ChangeSlotsPosWithPrevious(current + 1);
            current++;
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            pickUps++;
            countText.text = pickUps.ToString() + "/" + totalPickUps;
            countText.GetComponent<Animator>().Play("BigToSmall");
            AudioManager.instance.Play("PickUpPicked");
            GameManager.instance.PickedAPickUp(pickUps);
            if (pickUps == totalPickUps)
            {
                //winText.gameObject.SetActive(true);
                if (winBox)
                    winBox.ToggleOnOff();
            }
                
        }
    }


    public bool AddPowerUp(PowerUp item) {
        powerUps.Add(item);
        UpdateUIAfterAdd();
        return true;
    }

    public void UpdateUIAfterAdd() {
        inventoryUI.Add(powerUps[powerUps.Count - 1], (powerUps.Count).ToString());
        if (current >= 0)
            inventoryUI.Highlight(current);
    }

    public void DeactivatePowerUp() {
        foreach (PowerUp item in powerUps) {
            item.ReturnToNormal();
            item.enabled = false;
        }
    }

    public void ActivatePowerUp(int i) {
        if (i < 0 || i > powerUps.Count - 1)
            return;

        if(current == i)
        {
            powerUps[i].ActivatePowerUp();
            return;
        }
        DeactivatePowerUp();
        inventoryUI.Highlight(i);
        powerUps[i].enabled = true;
        current = i;
    }

}
