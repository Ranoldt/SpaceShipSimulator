using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship", menuName = "Agents/Ship")]
public class ShipObject : ShipDataObject
{
    public InventoryObject inv;

    public MineObjects miningTool;

    public InventoryObject inventory;

    public BoostComponent boost;

}
