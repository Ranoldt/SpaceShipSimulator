using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuildConfirmation : MonoBehaviour
{
    [SerializeField]
    private shipConfiguration editedShip;

    private void Awake()
    {
        //editedShip = GameManager.instance.playerShips[ShipBuilderManager.instance.initializedPlayers];
    }

    public void shipConfirm()
    {
        //need to get the selected parts and assign it to the scriptable object

    }
}
