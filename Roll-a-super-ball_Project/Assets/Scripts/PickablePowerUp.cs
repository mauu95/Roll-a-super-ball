using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickablePowerUp : MonoBehaviour
{
    public string type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(type == "dash")
            {
                other.gameObject.AddComponent<PowerUpDash>();
                other.GetComponent<Inventory>().AddPowerUp(other.gameObject.GetComponent<PowerUpDash>());
                other.gameObject.GetComponent<PowerUpDash>().enabled = false;
            }
            else if(type == "jump")
            {
                other.gameObject.AddComponent<PowerUpJump>();
                other.GetComponent<Inventory>().AddPowerUp(other.gameObject.GetComponent<PowerUpJump>());
                other.gameObject.GetComponent<PowerUpJump>().enabled = false;
            }

            Destroy(gameObject);
        }
    }

}
