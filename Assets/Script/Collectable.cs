using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag("Player") && gameObject.CompareTag("Collectable"))
        {
            CollectableCount.collectableCount++;
            Destroy(gameObject);
        }
    }
}
