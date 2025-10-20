using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject Counter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectableCount.collectableCount++;
            Destroy(gameObject);
            StartCoroutine(ShowCounter());
        }
    }

    private IEnumerator ShowCounter()
    {
        Counter.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        Counter.gameObject.SetActive(false);
    }
}


