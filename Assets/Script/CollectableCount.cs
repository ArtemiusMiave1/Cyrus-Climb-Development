using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI collectableCounter;
    [SerializeField] private int collectableTotal;
    [SerializeField] private Animator animator;

    public static int collectableCount;

    private int previousCount = 0;  // This is so it knows when the count changes

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            collectableCount = 0;
            VaultDoor.collectableCount=collectableCount;    // Vault Collection = collection count 
        }
        
        if (collectableCount != previousCount)  // Checking to see if the count has changed by comparing to the previous count
        {
            collectableCounter.text= collectableCount + "/" + collectableTotal;    // Updates the UI

            if (animator != null)
            {
                animator.SetTrigger("Collect");
            }

            previousCount = collectableCount;   // Updates previous count
        }
    }
}
