using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private PlayerManager player;
    [SerializeField]
    private Image radial;
    [SerializeField]
    private Image straight;

    [SerializeField]
    private const float radialFilled = 0.5f; //how much % of health is in the circular part


    // Update is called once per frame
    void Update()
    {
        radialFill();
        straightFill();
    }

    private void straightFill()
    {
        float fillThreshold = player.maxHealth * radialFilled;
        if(player.playerHealth >= fillThreshold)
        {
            straight.fillAmount = (player.playerHealth - fillThreshold)/fillThreshold;
        }
    }

    private void radialFill()
    {
        float filledHealth = player.maxHealth * radialFilled;
        
        radial.fillAmount = player.playerHealth / filledHealth;
        
    }
}
