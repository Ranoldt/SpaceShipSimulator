using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Checks a weapon scriptable object's Enum Type to see which behavior to apply
/// > if Beam: raycast beam logic
/// > if Bullet: projectile bullet logic
/// </summary>

public class MineToolFiring : MonoBehaviour
{
    private MineObjects tool { get { return GetComponent<SpaceShip>().inv.equippedMineTool; } }
    private bool held = false;

    [HideInInspector] public Transform[] shooters;
    //shooting points added here during initialization event

    [SerializeField]
    private Transform OriginMiddle;

    [SerializeField]
    private Event shootEvent;//a way to tell the ammo script to decrement ammo

    //variables critical for beam firing
    [SerializeField]
    private Event beamInitializationEvent;

    public List<LineRenderer> beams = new List<LineRenderer>();
    //beams added here during initialization event

    //variables for bullet firing
    [SerializeField]
    private Event bulletInitializationEvent;
    private float lastShootTime;

    private AmmoFiringLogic al;

    private void Start()
    {
        al = GetComponent<AmmoFiringLogic>();
        if (tool.mineType == mineToolType.beam)
        {
            beamInitializationEvent.Raise();
            //need to instantiate the line renderers when appropriate
        }
    }
    void Update()
    {
        if (tool.mineType == mineToolType.beam)
        {
            if (held && al.canFire)
            {
                BeamFire();
            }
            else if (!held)
            {
                OnBeamRelease();
            }
            else
            {
                OnBeamRelease();
            }
        }
        else if(tool.mineType == mineToolType.bullet)
        {
            if (held)
            {
                BulletFire();
            }
        }
    }

    private void BeamFire()
    {
        RaycastHit Hitinfo;


        if (TargetInfo.IsTargetInRange(OriginMiddle.position, OriginMiddle.forward, out Hitinfo, tool.laserRange, tool.shootingMask))
        {
            IShootable target = Hitinfo.transform.GetComponent<IShootable>();
            if (target != null)
            {
                //TODO: implement levels
                target.damage(tool.miningPower);//total power of laser
            }
            Instantiate(tool.laserHitParticles, Hitinfo.point, Quaternion.LookRotation(Hitinfo.normal));

            foreach (var beam in beams)
            {
                Vector3 localHitPosition = beam.transform.InverseTransformPoint(Hitinfo.point);
                beam.gameObject.SetActive(true);
                beam.SetPosition(1, localHitPosition);
            }
        }
        else
        {
            foreach (var beam in beams)
            {
                beam.gameObject.SetActive(true);
                beam.SetPosition(1, new Vector3(0, 0, tool.laserRange));
            }
        }
        shootEvent.Raise();
    }

    public void OnBeamRelease() //called in ammofiringlogic
    {
        foreach (LineRenderer beam in beams)
        {
            beam.gameObject.SetActive(false);
        }
    }

    private void BulletFire()
    {
        if (Time.time >= lastShootTime + tool.mineAmmoConfig.shootingInterval)
        {
            foreach (var firePoint in shooters)
            {
                Instantiate(tool.fab, firePoint);
            }
            lastShootTime = Time.time;
            shootEvent.Raise();
        }
    }

    //switch the status of held based on whether the button is being pressed or released. OnAttack is called every time the button is pressed and every time it is released, the if statements are what determine which of those two is currently happening.
    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            held = true;
        if (ctx.canceled)
            held = false;
    }
}
