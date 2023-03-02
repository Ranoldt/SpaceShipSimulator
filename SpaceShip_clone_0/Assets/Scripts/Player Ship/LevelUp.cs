using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script responsible for leveling up the ship's attributes.
/// </summary>
public class LevelUp : MonoBehaviour
{
    private BoostComponent boostdata;

    //variables for mine tool power

    [SerializeField]
    private FloatVariable currentCash;

    [SerializeField]
    private FloatVariable minePowerCost;

    [SerializeField]
    private FloatVariable minePowerLevel;

    [SerializeField]
    private Event MoneyChange;

    [SerializeField]
    private Event UpgradeChange;
    private void Start()
    {
        //initialize data 
        //boostdata = gameObject.GetComponent<SpaceShip>().shipdata.boost;

        //initialize levels
        minePowerLevel.SetValue(1);
        minePowerCost.SetValue(1000);

    }
    public void MinePowerLevelUp()
    {
        if (currentCash.FloatValue >= minePowerCost.FloatValue)
        {
            currentCash.DecrementValue(minePowerCost.FloatValue);
            minePowerLevel.IncrementValue(1);
            minePowerCost.SetValue(((int)Mathf.Round(minePowerCost.FloatValue * 1.5f)));
            MoneyChange.Raise();//update money UI by raising the event
            UpgradeChange.Raise(); //update cost UI in the shop menu
        }
    }

}
