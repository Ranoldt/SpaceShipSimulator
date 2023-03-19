using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script responsible for leveling up the ship's attributes.
/// </summary>
public class LevelUp : MonoBehaviour
{
    [SerializeField]
    private UnityEvent inventoryUpgradeResponse;

    [SerializeField]
    private InventoryManager inventory;

    [SerializeField]
    private PlayerManager player;

    //cost variables
    public int minePowerCost { get; private set;}
    public int mineAmmoCost { get; private set; }

    public int boostPowerCost { get; private set; }
    public int boostCapacityCost { get; private set; }

    public int invCapacityCost { get; private set; }

    //level variables (they basically act as global multipliers to your attributes)
    public int minePowerLevel { get; private set; }
    public int mineAmmoLevel { get; private set; }

    public int boostPowerLevel { get; private set; }
    public int boostCapacityLevel { get; private set; }


    private void Awake()
    {
        //initialize starting prices
        minePowerCost = 500;
        mineAmmoCost = 500;

        boostPowerCost = 300;
        boostCapacityCost = 300;

        invCapacityCost = 1000;
    }


    [SerializeField]
    private Event MoneyChange;
    [SerializeField]
    private Event onUpgrade;

    public void MinePowerLevelUp()
    {
        if (inventory.currentCash >= minePowerCost)
        {
            inventory.currentCash -= minePowerCost;
            minePowerLevel+= 1;
            minePowerCost = (int)Mathf.Round(minePowerCost * 1.5f);
            MoneyChange.Raise();//update money UI by raising the event
            onUpgrade.Raise();
        }
    }

    public void MineAmmoLevelUp()
    {
        if (inventory.currentCash >= mineAmmoCost)
        {
            inventory.currentCash -= mineAmmoCost;
            mineAmmoLevel += 1;
            mineAmmoCost = (int)Mathf.Round(mineAmmoCost * 1.5f);
            player.ammoCapacity += (5 * mineAmmoLevel);
            player.ammoLeft = player.ammoCapacity;
            MoneyChange.Raise();//update money UI by raising the event
            onUpgrade.Raise();
        }
    }

    public void BoostPowerLevelUp()
    {
        if (inventory.currentCash >= boostPowerCost)
        {
            inventory.currentCash -= boostPowerCost;
            boostPowerLevel += 1;
            boostPowerCost = (int)Mathf.Round(boostPowerCost * 1.5f);
            MoneyChange.Raise();//update money UI by raising the event
            onUpgrade.Raise();
        }
    }

    public void BoostCapacityLevelUp()
    {
        if (inventory.currentCash >= boostCapacityCost)
        {
            inventory.currentCash -= boostCapacityCost;
            boostCapacityLevel += 1;
            boostCapacityCost = (int)Mathf.Round(boostCapacityCost * 1.5f);
            player.boostCapacity += (5 * boostCapacityLevel);
            player.boostLeft = player.boostCapacity;
            MoneyChange.Raise();//update money UI by raising the event
            onUpgrade.Raise();
        }
    }

    public void InventoryLevelUp()
    {
        if (inventory.currentCash >= invCapacityCost)
        {
            inventory.currentCash -= invCapacityCost;
            inventory.InvSize += 1;
            invCapacityCost = (int)Mathf.Round(invCapacityCost * 1.25f);
            MoneyChange.Raise();//update money UI by raising the event
            onUpgrade.Raise();
            inventoryUpgradeResponse.Invoke(); //update inventory UI in shop and in field
        }
    }

}
