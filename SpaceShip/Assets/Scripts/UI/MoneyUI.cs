using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text moneyText;

    [SerializeField]
    private InventoryManager inv;

    private int moneyAmount { get { return inv.currentCash; } }
    public void UpdateText()
    {
        moneyText.text = "$:" + moneyAmount.ToString();
    }
}
