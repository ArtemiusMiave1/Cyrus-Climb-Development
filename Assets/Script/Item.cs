using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;

    [SerializeField] private int quantity;

    [SerializeField] private Sprite sprite;

    [TextArea]
    [SerializeField] private string itemDescription;

    private InventoryManager inventoryManager;


    private bool collected = false;            // <--- guard

    void Start()
    {
        inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;                 // already collected — ignore
        if (!other.CompareTag("Player")) return;

        collected = true;                      // mark as collected immediately

        // Prevent any further triggers in the same frame
        Collider c = GetComponent<Collider>();
        if (c != null) c.enabled = false;

        // optional defensive null-check
        if (inventoryManager == null)
        {
            Debug.LogWarning("InventoryManager not found on Canvas!");
        }
        else
        {
            inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
        }

        CollectableCount.collectableCount++;
        VaultDoor.collectableCount++;

        Destroy(gameObject);                   // still fine to destroy
    }
}
