using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //TODO: implement stack size and inv size

    /// <summary>
    /// This script is only responsible for adding new items in the inventory on collision with an item object.
    /// The actual list logic is stored in the InventoryObject, and the UI component is dealt with in the Inventory Menu script
    /// </summary>
    [SerializeField]
    private FloatVariable invSize;

    private InventoryObject inventory;

    private void Start()
    {
        inventory = gameObject.GetComponent<SpaceShip>().shipdata.inv;
        invSize.SetValue(inventory.MinSize.FloatValue);
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
        inventory.currentCash.SetValue(0);
    }
}
