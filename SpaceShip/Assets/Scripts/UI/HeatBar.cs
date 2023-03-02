using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatBar : MonoBehaviour
{
    [SerializeField]
    private PlayerManager player;

    [SerializeField]
    private Slider Bar;

    // Update is called once per frame
    void Update()
    {
        Bar.value = (player.ammoCapacity-player.ammoLeft) / player.ammoCapacity;
    }
}
