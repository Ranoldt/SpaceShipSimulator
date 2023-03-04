using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text moneyText;

    private InventoryManager inv { get { return this.gameObject.GetComponentInParent<UIManager>().inv; } }

    private int moneyAmount { get { return inv.currentCash; } }
    public void UpdateText()
    {
        moneyText.text = "$:" + moneyAmount.ToString();
    }
}
