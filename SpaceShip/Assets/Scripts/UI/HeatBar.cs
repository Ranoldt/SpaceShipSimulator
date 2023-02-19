using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatBar : MonoBehaviour
{
    [SerializeField]
    private FloatVariable HeatValue;

    [SerializeField]
    private FloatVariable HeatMax;

    [SerializeField]
    private Slider Bar;

    // Update is called once per frame
    void Update()
    {
        Bar.value = HeatValue.FloatValue / HeatMax.FloatValue;
    }
}
