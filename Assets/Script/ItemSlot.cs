using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //===ITEM DATA===//
    public string itemName;
    public int quantity;
    public bool isFull;
    public Sprite itemSprite;
    public string itemDescription;
    public Sprite emptySprite;


    //===ITEM SLOT===//
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemNameText;


    //===ITEM DESCRIPTION SLOT===//
    public TMP_Text ItemDescriptionText;


    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    public void Start()
    {
        inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();

        //Debug.Log(quantityText.enabled);
    }
    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;

        itemNameText.text = itemName;
        itemNameText.enabled = true;
    
        //Debug.Log(quantityText.enabled);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.DeslectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
        ItemDescriptionText.text = itemDescription;
        itemImage.sprite = itemSprite;
        itemNameText.text = itemName;
    }

    public void OnRightClick()
    {
    
    }
}
