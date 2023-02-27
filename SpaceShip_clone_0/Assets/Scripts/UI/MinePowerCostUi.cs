using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinePowerCostUi : MonoBehaviour
{
    [SerializeField]
    private TMP_Text shoptext;

    [SerializeField]
    private FloatVariable cost;

    private void Start()
    {
        shoptext.text = "$: " + cost.FloatValue.ToString();
    }
    public void updateCost()
    {
        shoptext.text ="$: " + cost.FloatValue.ToString();
    }
}
