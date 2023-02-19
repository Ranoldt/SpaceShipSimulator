using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostBar : MonoBehaviour
{
    //behavior that adjusts a slider showing how much boost you have left

    [SerializeField]
    private FloatVariable boostMax;
    [SerializeField]
    private FloatVariable BoostLeft;
    [SerializeField]
    private Slider boostBar;



    // Start is called before the first frame update
    void Start()
    {
        boostBar.value = 1;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        boostBar.value = BoostLeft.FloatValue / boostMax.FloatValue; //value made as a percentage so you dont have to adjust the values every time boostTime changes
    }
}
