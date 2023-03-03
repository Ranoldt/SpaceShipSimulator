using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// It needs to spawn the amount of slot objects equal to the current
/// inventory's size.
/// 
/// Maybe raise an event when it's full and a player tries to add more stuff
/// </summary>
/// 

public class InventoryMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject invUi;

    [SerializeField]
    private GameObject slot;

    private InventoryManager inventoryData { get { return this.gameObject.GetComponentInParent<UIManager>().inv; } }

    private Slot[] slots;


    private void Start()
    {
        ConstructUI();
        UpdateUI(); //just making sure the UI's updated on start
        slots = invUi.GetComponentsInChildren<Slot>();
        inventoryData.OnItemChangedCallback += UpdateUI; //subscribe to an event set up in InventoryObject
    }

    private void ConstructUI()
    {
        for(int i = 0; i < inventoryData.InvSize; i++)
        {
            var spawnedSlot = Instantiate(slot);
            spawnedSlot.transform.SetParent(this.gameObject.transform.GetChild(0).transform, false); 
            //make the slot the child of the inventory panel
        }
    }

    private void UpdateUI()
    {
        slots = invUi.GetComponentsInChildren<Slot>(); // remake the array because the contents of the Inv changes
        //read the inventory list and populate the next available slot if it sees a new object
        for (int i = 0; i < inventoryData.Container.Count; i++)
        {
            slots[i].AddItem(inventoryData.Container[i].item, inventoryData.Container[i].amount);
        }
        if (inventoryData.Container.Count == 0)
        {
            for (int i = slots.Length - 1; i >= 0; i--)
            {
                slots[i].ClearSlot();
            }
        }

    }


    public void InvToggle()
    {
        if (invUi.activeSelf)
        {
            invUi.SetActive(false);
        }
        else
        {
            invUi.SetActive(true);
        }
    }
}
