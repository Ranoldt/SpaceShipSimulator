using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship", menuName = "Agents/Ship")]
public class ShipObject : ShipDataObject
{
    public InventoryObject inv { get { return _inv; } private set { _inv = value; } }
    [SerializeField]
    private InventoryObject _inv;



    //Left public because of character customization
    public MineObjects miningTool;

    public BoostComponent boost;

}
