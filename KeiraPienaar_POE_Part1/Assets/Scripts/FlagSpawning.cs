using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSpawning : MonoBehaviour
{
    public GameObject flagPrefabAI;
    public GameObject flagPrefabPlayer;

    public Transform spawnPoint1;
    public Transform spawnPoint2;

    void Awake()
    {
        SpawnFlags();
       
    }

    public void SpawnFlags() {
        // Spawn the first flag at spawnPoint1
        Instantiate(flagPrefabAI, spawnPoint1.position, Quaternion.identity);

        // Spawn the second flag at spawnPoint2
        Instantiate(flagPrefabPlayer, spawnPoint2.position, Quaternion.identity);
    }
}
