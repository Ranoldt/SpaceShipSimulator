using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CostUi : MonoBehaviour
{

    [SerializeField]
    private LevelUp levelManager;
    private enum costType
    {
        MinePower,
        MineCapacity,
        BoostPower,
        BoostCapacity,
        Inventory,
    }

    [SerializeField]
    private costType _costType;

    [SerializeField]
    private TMP_Text shoptext;

    [SerializeField]
    private int cost;

    private void Start()
    {
        updateCost();
    }
    public void updateCost()
    {
        shoptext.text = "$: " + GetCost().ToString();
    }

    private int GetCost()
    {
        switch (_costType)
        {
            case costType.MinePower:
                cost = levelManager.minePowerCost;
                break;
            case costType.MineCapacity:
                cost = levelManager.mineAmmoCost;
                break;
            case costType.BoostPower:
                cost = levelManager.boostPowerCost;
                break;
            case costType.BoostCapacity:
                cost = levelManager.boostCapacityCost;
                break;
            case costType.Inventory:
                cost = levelManager.invCapacityCost;
                break;
        }
        return cost;
    }
}
