using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletShooting : MonoBehaviour
{
    private MineObjects beamdata;

    [SerializeField]
    private FloatVariable minePower;

    //variables for levelling mining power
    [SerializeField]
    private float laserIntervals;

    private float nextLaserTime;

    private bool shootable;

    [SerializeField]
    private FloatVariable laserLevel;

    [SerializeField]
    private FloatVariable cost;


    [SerializeField]
    private Transform[] LaserOrigin;

    [SerializeField]
    private Transform OriginMiddle;

    public float _miningPower { get { return beamdata.miningPower; } private set { _miningPower = value; } }

    [SerializeField]
    private FloatVariable currentlaserheat;
    private bool overHeated = false;
    private bool firing;

    public float Currentlaserheat
    {
        get { return currentlaserheat.FloatValue; }
    }

    [SerializeField]
    private FloatVariable LaserHeatThreshold;
    private void Awake()
    {
        //beamdata = gameObject.GetComponent<SpaceShip>().shipdata.miningTool; //access player ship data's mining tool component
        // The component is self contained within the prefab- there are no unnecessary dependencies.

        //LaserHeatThreshold.SetValue(beamdata.laserHeatThreshold); //initialize the float variable for UI to see the value
        //remember to also initialize it whenever this value changes (like when you upgrade the threshold)

        minePower.SetValue(beamdata.miningPower);
    }

    private void Update()
    {
        if(Time.time >= nextLaserTime)
        {
            shootable = true;
        }

        LaserFiring();
    }
    private void LaserFiring()
    {
        if (firing && !overHeated && shootable)
        {
            FireLaser();
        }
        else
        {
            CoolLaser();
        }
    }

    void FireLaser()
    {
        foreach (var firePoint in LaserOrigin)
        {
            var bulletFab = beamdata as BulletType;
            Instantiate(bulletFab.bullet, firePoint.transform);
            shootable = false;
            nextLaserTime = Time.time + laserIntervals;
        }
        
        HeatLaser();

    }

    void HeatLaser()
    {
        //if (firing && currentlaserheat.FloatValue < beamdata.laserHeatThreshold)
        //{
        //    currentlaserheat.IncrementValue( beamdata.laserHeatRate);

        //    if (currentlaserheat.FloatValue >= beamdata.laserHeatThreshold)
        //    {
        //        overHeated = true;
        //        firing = false;
        //    }
        //}
    }

    void CoolLaser()
    {
        //if (overHeated)
        //{
        //    if (currentlaserheat.FloatValue / beamdata.laserHeatThreshold <= 0.5f)
        //    {
        //        overHeated = false;
        //    }
        //}

        //if (currentlaserheat.FloatValue > 0f)
        //{
        //    currentlaserheat.FloatValue -= beamdata.laserCoolRate * Time.deltaTime;
        //}

    }

    public void OnFire(InputAction.CallbackContext context)
    {
        firing = context.performed;
    }



}
