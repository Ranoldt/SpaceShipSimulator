using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text moneyText;
    [SerializeField]
    private FloatVariable moneyAmount;
    public void UpdateText()
    {
        moneyText.text = "$:" + moneyAmount.FloatValue.ToString();
    }
}
