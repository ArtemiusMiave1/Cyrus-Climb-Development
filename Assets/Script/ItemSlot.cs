using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour //, IPointerClickHandler
{
    //===ITEM DATA===//
    public string itemName;
    public int quantity;
    public bool isFull;
    public Sprite itemSprite;
    //public string itemDescription;


    //===ITEM SLOT===//
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    //===ITEM DESCRIPTION SLOT===//
    // public TMP_Text itemDescriptionNameText;
    //public TMP_Text ItemDescriptionText;

    //public GameObject selectedShader;
    //public bool thisItemSelected;

    //private InventoryManager inventoryManager;
    //private void Start()
    //{
    //    inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>;
    //}

    public void Start()
    {
        Debug.Log(quantityText.enabled);
    } 
    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
        //Debug.Log(quantityText.enabled);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
