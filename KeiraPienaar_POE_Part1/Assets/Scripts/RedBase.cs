using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBase : MonoBehaviour
{
    public bool enemyFlagReturned = false;
    public GameObject flag;
    public Transform EnemySpawn;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
           
            EnemyStateMachine enemyfsm = collision.GetComponent<EnemyStateMachine>();
            if (enemyfsm != null && enemyfsm.redFlagCollected == true)
            {
               enemyFlagReturned = true;
               
                Debug.Log("enemy has a red flag!");
               // flag.transform.SetParent(null);
               // reset the enemy 
            }
            else
            {
                
                Debug.Log("Player does not have a red flag!");
            }
        }


    }
}
