using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class EquipSlot : MonoBehaviour
{
    [SerializeField]
    private TMP_Text equipmentName;

    [SerializeField]
    private Image icon;

    ShipPart item;

    public void AddItem(ShipPart obj, string name)
    {
        item = obj;
        equipmentName.text = name;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    // these slots are permanent, so we don't have any need to remove slots
}
