using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Ore Object", menuName = "Inventory System/Items/Ore")]
public class OreObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.ore;

    }
}
