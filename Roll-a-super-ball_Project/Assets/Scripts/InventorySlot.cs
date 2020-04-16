using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image cooldownImage;
    public float cooldown;
    private bool isCooldown;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            isCooldown = true;

        if (isCooldown)
        {
            cooldownImage.fillAmount += 1 / cooldown * Time.deltaTime;

            if (cooldownImage.fillAmount >= 1)
            {
                cooldownImage.fillAmount = 0;
                isCooldown = false;
            }

        }
    }
}
