using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Heal Object", menuName = "Inventory System/Items/Heal")]
public class HealObject : ItemObject
{
    public float healthHealValue;
    public float boostHealValue;
    private void Awake()
    {
        type = ItemType.heal;

    }
}
