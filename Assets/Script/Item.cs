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


    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas == null)
        {
            Debug.LogError("Canvas GameObject not found! Please check if it's present in the scene.");
        }
        else
        {
            inventoryManager = canvas.GetComponent<InventoryManager>();

            if (inventoryManager == null)
            {
                Debug.LogError("InventoryManager component not found on the Canvas GameObject! Please attach the InventoryManager script.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            CollectableCount.collectableCount++;
            Destroy(gameObject);
        }
    }
}
