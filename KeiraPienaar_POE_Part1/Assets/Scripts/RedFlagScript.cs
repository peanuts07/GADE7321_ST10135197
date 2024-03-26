using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFlagScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
      

        if (other.CompareTag("Enemy"))
        {
            EnemyStateMachine fsm = other.GetComponent<EnemyStateMachine>();
            if (fsm != null)
            {
                fsm.redFlagCollected = true; // change the logic
            }
            AttachFlagToPlayer(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                // Set the boolean variable in the player controller script to true
                playerController.SpawnEnemyFlagAtBase(this.transform);

               
            }


           
        }
    }

    private void AttachFlagToPlayer(GameObject player)
    {
        // Attach the flag to the player by making it a child of the player's GameObject
        transform.parent = player.transform;

        // Disable the flag's collider to prevent it from triggering again
        GetComponent<Collider2D>().enabled = false;

        // Optionally, you can perform additional actions here such as updating the player's state
        // For example, you could call a method on the player's controller to indicate that the player now has a flag
        // player.GetComponent<PlayerController>().FlagPickedUp();
    }
}
