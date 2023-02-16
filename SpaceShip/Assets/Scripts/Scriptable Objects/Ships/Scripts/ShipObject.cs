using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship", menuName = "Agents/Ship")]
public class ShipObject : ShipDataObject
{
    public InventoryObject inv;

    public GameObject mininglaser;

    public int miningLevel;
    public int boostLevel;
    public int moveLevel;

}
