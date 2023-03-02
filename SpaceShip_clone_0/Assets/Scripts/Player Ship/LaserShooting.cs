using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserShooting : MonoBehaviour
{
    private MineObjects beamdata;

    //variables for levelling mining power
    [SerializeField]
    private FloatVariable laserLevel;

    [SerializeField]
    private FloatVariable cost;


    [SerializeField]
    private Transform[] LaserOrigin;
  
    [SerializeField]
    private Transform OriginMiddle;

    [SerializeField]
    private LineRenderer[] lasers;
    
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
        beamdata = gameObject.GetComponent<SpaceShip>().shipdata.miningTool; //access player ship data's mining tool component
        // The component is self contained within the prefab- there are no unnecessary dependencies.

        //LaserHeatThreshold.SetValue(beamdata.laserHeatThreshold); //initialize the float variable for UI to see the value
        //remember to also initialize it whenever this value changes (like when you upgrade the threshold)
    }

    private void Update()
    {
       LaserFiring();
       
    }

    private void LaserFiring()
    {
        if (firing && !overHeated)
        {
            FireLaser();
        }
        else
        {
            foreach(var laser in lasers)
            {
                laser.gameObject.SetActive(false);
            }
            CoolLaser();
        }
    }

    void FireLaser()
    {
        
        RaycastHit Hitinfo;


        if(TargetInfo.IsTargetInRange(OriginMiddle.transform.position,OriginMiddle.transform.forward, out Hitinfo, beamdata.laserRange, beamdata.shootingMask))
        {
            IShootable target = Hitinfo.transform.GetComponent<IShootable>();
            if(target != null)
            {
                target.damage(beamdata.miningPower + (1.5f * laserLevel.FloatValue));//total power of laser
            }
            Instantiate(beamdata.laserHitParticles, Hitinfo.point, Quaternion.LookRotation(Hitinfo.normal));

            foreach(var laser in lasers)
            {
                Vector3 localHitPosition = laser.transform.InverseTransformPoint(Hitinfo.point);
                laser.gameObject.SetActive(true);
                laser.SetPosition(1, localHitPosition);
            }    
        }
        else
        {
            foreach(var laser in lasers)
            {
                laser.gameObject.SetActive(true);
                laser.SetPosition(1, new Vector3(0, 0, beamdata.laserRange));
            }
        }
        HeatLaser();
        
    }

    void HeatLaser()
    {
        //if (firing && currentlaserheat.FloatValue < beamdata.laserHeatThreshold)
        //{
        //    currentlaserheat.FloatValue += beamdata.laserHeatRate * Time.deltaTime;

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
        
        //    if (currentlaserheat.FloatValue > 0f)
        //    {
        //        currentlaserheat.FloatValue -= beamdata.laserCoolRate * Time.deltaTime;
        //    }
        
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        firing = context.performed;
    }

   

}
