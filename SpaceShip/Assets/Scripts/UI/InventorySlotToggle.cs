using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// when toggled, tells the inventory to make that item selected
/// </summary>
public class InventorySlotToggle : MonoBehaviour
{
    private InventoryManager inventory;

    private void Start()
    {
        inventory = this.transform.root.GetComponentInChildren<InventoryManager>();
    }

    public void OnToggle(bool isSelected)
    {
        if(inventory != null && isSelected)
            inventory.selectItem(this.gameObject.GetComponentInParent<Slot>().index);
    }
}
