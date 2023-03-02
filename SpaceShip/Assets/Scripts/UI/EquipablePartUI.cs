using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipablePartUI : MonoBehaviour
{
    [SerializeField]
    private GameObject slot;

    [SerializeField]
    private InventoryManager inventory;

    // Start is called before the first frame update
    void OnEnable()
    {
        UpdateMineUI();
        //inventory.OnMineToolBoughtCallback += UpdateMineUI;
    }

    private void UpdateMineUI()
    {
        var newSlot = Instantiate(slot);
        newSlot.transform.SetParent(this.gameObject.transform, false);

    }

    private void OnDisable()
    {
        inventory.OnMineToolBoughtCallback -= UpdateMineUI;
    }
}
