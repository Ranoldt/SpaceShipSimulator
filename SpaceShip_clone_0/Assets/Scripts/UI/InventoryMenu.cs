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

    [SerializeField]
    private InventoryManager inventoryData;

    private Slot[] slots;
    private int slotIndex = 0;


    private void Start()
    {
        for (int i = 0; i < inventoryData.InvSize; i++)
            ConstructUI();
        UpdateUI(); //just making sure the UI's updated on start
        slots = invUi.GetComponentsInChildren<Slot>();
        inventoryData.OnItemChangedCallback += UpdateUI; //subscribe to an event set up in InventoryObject
    }

    public void ConstructUI()
    {
        var spawnedSlot = Instantiate(slot);
        spawnedSlot.transform.SetParent(this.gameObject.transform.GetChild(0).transform, false);
        spawnedSlot.GetComponent<Slot>().setIndex(slotIndex);
        slotIndex += 1;
        //make the slot the child of the inventory panel
    }

    private void UpdateUI()
    {
        slots = invUi.GetComponentsInChildren<Slot>(); // remake the array because the contents of the Inv changes
        //read the inventory list and populate the next available slot if it sees a new object
        foreach(Slot slot in slots)
        {
            slot.ClearSlot();
        }
        
        for (int i = 0; i < inventoryData.Container.Count; i++)
        {
            slots[i].AddItem(inventoryData.Container[i].item, inventoryData.Container[i].amount);
        }

    }

    public void UntoggleSelectibles()
    {
        foreach (Slot slot in slots)
        {
            var toggle = slot.gameObject.GetComponentInChildren<UnityEngine.UI.Toggle>();
            if (toggle != null)
            {
                toggle.isOn = false;
            }
        }
    }


    public void InvToggle(InputAction.CallbackContext context)
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
