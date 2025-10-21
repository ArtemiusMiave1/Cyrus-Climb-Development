using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player") && gameObject.CompareTag("Collectable")) // Compares the player and collectable tags
    {
        // Debug.Log("Collectable triggered: " + gameObject.name);
        CollectableCount.collectableCount++;    // Collectable count goes up by 1
            VaultDoor.collectableCount++;    // Collectable count goes up by 1
            Destroy(gameObject);
    }
}
}
