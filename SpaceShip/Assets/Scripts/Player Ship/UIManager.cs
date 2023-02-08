using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Slider LaserHeat;
    [SerializeField] private LaserShooting lasershooting;

    // Start is called before the first frame update
    private void Start()
    {
        lasershooting = FindObjectOfType<LaserShooting>();
     }

    // Update is called once per frame
    private void Update()
    {
        if(lasershooting != null)
        {
            LaserHeat.value = lasershooting.Currentlaserheat / lasershooting.LaserHeatThreshold;
        }
    }
}
