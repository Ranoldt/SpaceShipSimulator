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

    private float currentlaserheat = 0f;
    private bool overHeated = false;
    private bool firing;
    private Camera playercamera;

    public float Currentlaserheat
    {
        get { return currentlaserheat; }
    }

    public float LaserHeatThreshold
    {
        get { return beamdata.laserHeatThreshold; }
    }
    private void Awake()
    {
        playercamera = Camera.main;
        totalPower = beamdata.miningPower; //variable initialization
        cost = 1000;
        costText.text = "Cost: $" + cost.ToString();
    }

    private void Update()
    {
       LaserFiring();
       
    }

    public void LevelUpLaser()
    {
        if (inventory.currentCash >= cost)
        {
            inventory.currentCash -= cost;
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
        if (firing && currentlaserheat < beamdata.laserHeatThreshold)
        {
            currentlaserheat += beamdata.laserHeatRate * Time.deltaTime;

            if (currentlaserheat >= beamdata.laserHeatThreshold)
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
            if (currentlaserheat / beamdata.laserHeatThreshold <= 0.5f)
            {
                overHeated = false;
            }
        }
        
            if (currentlaserheat > 0f)
            {
                currentlaserheat -= beamdata.laserCoolRate * Time.deltaTime;
            }
        
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        firing = context.performed;
    }

   

}
