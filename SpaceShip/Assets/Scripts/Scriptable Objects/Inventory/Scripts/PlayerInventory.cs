using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    /// <summary>
    /// This script is only responsible for adding new items in the inventory on collision with an item object.
    /// </summary>

    [SerializeField]private InventoryManager inventory;

    private void Start()
    {

        //initialize the player's component inventories
        inventory.miningToolContainer.Add(inventory.equippedMineTool);
        if (inventory.OnMineToolBoughtCallback != null)
            inventory.OnMineToolBoughtCallback.Invoke();

        inventory.boostContainer.Add(inventory.equippedBoost);
        if (inventory.OnBoostBoughtCallback != null)
            inventory.OnBoostBoughtCallback.Invoke();
    }

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();

        if (item)
        {
            var doAdd = inventory.addItem(item.item, 1);
            if (doAdd)
            {
                //if adding an item is successful, play a sound
                AudioManager.instance.PlayOneShot(FMODEvents.instance.iPickSfx, this.transform.position);
                other.GetComponent<Collider>().enabled = false; //deal with double counting by making sure on trigger enter only happens once
                Destroy(other.gameObject);
            }
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        inventory.miningToolContainer.Clear();
        inventory.boostContainer.Clear();
    }
}
