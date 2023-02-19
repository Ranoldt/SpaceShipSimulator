using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class LaserShooting : MonoBehaviour
{
    [SerializeField]
    private BeamType beamdata;

    //variables for levelling mining power
    private int laserLevel;
    private float totalPower;
    private int cost;

    [SerializeField]
    InventoryObject inventory;
    [SerializeField]
    private TMP_Text costText;


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
        totalPower = beamdata.miningPower; //variable initialization
        cost = 1000;
        costText.text = "Cost: $" + cost.ToString();

        LaserHeatThreshold.SetValue(beamdata.laserHeatThreshold); //initialize the float variable for UI to see the value
        //remember to also initialize it whenever this value changes (like when you upgrade the threshold)
    }

    private void Update()
    {
       LaserFiring();
       
    }

    public void LevelUpLaser()
    {
        if (inventory.currentCash.FloatValue >= cost)
        {
            inventory.currentCash.DecrementValue (cost);
            laserLevel += 1;
            totalPower = beamdata.miningPower + (1.5f * laserLevel);
            cost = ((int)Mathf.Round(cost * 1.5f));
            costText.text = "Cost: $" + cost.ToString();
        }
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
                target.damage(totalPower);
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
        if (firing && currentlaserheat.FloatValue < beamdata.laserHeatThreshold)
        {
            currentlaserheat.FloatValue += beamdata.laserHeatRate * Time.deltaTime;

            if (currentlaserheat.FloatValue >= beamdata.laserHeatThreshold)
            {
                overHeated = true;
                firing = false;
            }
        }
    }

    void CoolLaser()
    {
        if (overHeated)
        {
            if (currentlaserheat.FloatValue / beamdata.laserHeatThreshold <= 0.5f)
            {
                overHeated = false;
            }
        }
        
            if (currentlaserheat.FloatValue > 0f)
            {
                currentlaserheat.FloatValue -= beamdata.laserCoolRate * Time.deltaTime;
            }
        
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        firing = context.performed;
    }

   

}
