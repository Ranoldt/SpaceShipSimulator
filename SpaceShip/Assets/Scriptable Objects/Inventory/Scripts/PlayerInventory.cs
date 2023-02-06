using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    Transform itemParent;


    private Slot[] slots;

    public InventoryObject inventory;

    private void Start()
    {
        inventory.OnItemChangedCallback += UpdateUI;

        slots = itemParent.GetComponentsInChildren<Slot>();
        //CreateUI();
    }

    //private void CreateUI()
    //{
        
    //}

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
            inventory.addItem(item.item, 1);
        }

        Destroy(other.gameObject);
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
