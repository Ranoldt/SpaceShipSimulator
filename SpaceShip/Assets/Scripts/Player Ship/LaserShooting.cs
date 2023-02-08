using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserShooting : MonoBehaviour
{
    [SerializeField]
    private Transform[] LaserOrigin;
    [SerializeField]
    private LayerMask shootingMask;
    [SerializeField]
    private float laserRange = 100f;
    private bool targetInRange = false;
    [SerializeField]
    private Transform OriginMiddle;

    [SerializeField]
    private LineRenderer[] lasers;
    [SerializeField]
    private ParticleSystem laserHitParticles;
    [SerializeField]
    private float miningPower = 1f;
    public float _miningPower { get { return miningPower; } private set { _miningPower = value; } }


    [SerializeField]
    private float laserHeatThreshold = 2f;
    [SerializeField]
    private float laserHeatRate = 0.25f;
    [SerializeField]
    private float laserCoolRate = 0.5f;
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
        get { return laserHeatThreshold; }
    }
    private void Awake()
    {
        playercamera = Camera.main;
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


        if(TargetInfo.IsTargetInRange(OriginMiddle.transform.position,OriginMiddle.transform.forward, out Hitinfo, laserRange, shootingMask))
        {
            IShootable target = Hitinfo.transform.GetComponent<IShootable>();
            if(target != null)
            {
                target.damage(miningPower);
            }
            Instantiate(laserHitParticles, Hitinfo.point, Quaternion.LookRotation(Hitinfo.normal));

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
                laser.SetPosition(1, new Vector3(0, 0, laserRange));
            }
        }
        HeatLaser();
        
    }

    //IEnumerator ShootLaser()
    //{
    //    foreach(var laser in lasers)
    //    {
    //        laser.gameObject.SetActive(true);
    //    }
    //    yield return new WaitForSeconds(laserHeatThreshold);
    //    foreach (var laser in lasers)
    //    {
    //        laser.gameObject.SetActive(false);
    //    }
    //}
    void HeatLaser()
    {
        if (firing && currentlaserheat < laserHeatThreshold)
        {
            currentlaserheat += laserHeatRate * Time.deltaTime;

            if (currentlaserheat >= laserHeatThreshold)
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
            if (currentlaserheat / laserHeatThreshold <= 0.5f)
            {
                overHeated = false;
            }
        }
        
            if (currentlaserheat > 0f)
            {
                currentlaserheat -= laserCoolRate * Time.deltaTime;
            }
        
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        firing = context.performed;
    }

   

}
