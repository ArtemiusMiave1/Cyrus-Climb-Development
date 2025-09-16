using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]  private string itemName;

    [SerializeField]  private int quantity;

    [SerializeField]  private Sprite sprite;

    private InventoryManager inventoryManager;


    void Start()
    {
        inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventoryManager.AddItem(itemName, quantity, sprite);
            CollectableCount.collectableCount++;
            Destroy(gameObject);
        }
    }
}
