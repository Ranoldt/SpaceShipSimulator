using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    Transform itemParent;
    [SerializeField]
    private TMP_Text money;


    private Slot[] slots;

    public InventoryObject inventory;

    private void Start()
    {
        inventory.OnItemChangedCallback += UpdateUI;

        slots = itemParent.GetComponentsInChildren<Slot>();
    }

    private void Update()
    {
        money.text = "$" + inventory.currentCash.ToString(); //update ui
    }
    public void sellItems()
    {
        for (int i = inventory.Container.Count-1; i >= 0; i--)
        {
            int stackPrice = inventory.Container[i].item.SellAmount * inventory.Container[i].amount;
            inventory.currentCash += stackPrice;
            inventory.Container.RemoveAt(i);
            slots[i].ClearSlot();
        }

    }
    private void UpdateUI()
    {
        slots = itemParent.GetComponentsInChildren<Slot>();
        //read the inventory list and populate the next available slot if it sees a new object
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            slots[i].AddItem(inventory.Container[i].item,inventory.Container[i].amount);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();

        if (item)
        {
            other.GetComponent<Collider>().enabled = false; //deal with double counting by making sure on trigger enter only happens once
            inventory.addItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        inventory.currentCash = 0;
    }
}
