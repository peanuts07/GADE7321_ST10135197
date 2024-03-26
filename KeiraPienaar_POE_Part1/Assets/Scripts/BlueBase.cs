using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBase : MonoBehaviour
{
    public bool flagReturned = false;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.CompareTag("Player"))
        {
            // Check if the player has a blue flag
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null && playerController.blueFlagCollected ==true)
            {
                flagReturned = true;
               
                // Player has a blue flag
                Debug.Log("Player has a blue flag!");

                // Perform actions specific to having a blue flag, such as capturing it
            }
            else
            {
                // Player does not have a blue flag
                Debug.Log("Player does not have a blue flag!");
            }
        }

    }
}
