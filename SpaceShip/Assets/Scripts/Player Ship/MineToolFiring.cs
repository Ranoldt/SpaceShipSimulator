using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

/// <summary>
/// Checks a weapon scriptable object's Enum Type to see which behavior to apply
/// > if Beam: raycast beam logic
/// > if Bullet: projectile bullet logic
/// </summary>
/// 

//Beam initialization method is in the LaserOrigin script.

public class MineToolFiring : MonoBehaviour
{
    [SerializeField]
    private AudioSource sfxSource;

    [SerializeField]
    private AudioClip bulletSfx;

    [SerializeField]
    private AudioClip beamSfx;

    private MineObjects tool { get { return GetComponent<SpaceShip>().inv.equippedMineTool; } }
    private bool held = false;

    [SerializeField]
    private Transform[] shooters;
    //shooting points added here during initialization event for both beams and bullets

    [SerializeField]
    private Transform OriginMiddle;

    public List<LineRenderer> beams = new List<LineRenderer>();
    //beams added here during initialization event
    //this list is iterated through whenever these beams are activated/ deactivated

    private float lastShootTime;

    private MineAmmoConfig ammoConfig;
    private MineObjects minetool;

    private PlayerManager manager;
    public bool canFire;

    [SerializeField]
    private LevelUp levels;

    IObjectPool<ParticleSystem> particlePool;
    // Start is called before the first frame update
    void Start()
    {
        canFire = true;
        minetool = GetComponent<SpaceShip>().inv.equippedMineTool;
        ammoConfig = minetool.mineAmmoConfig;

        manager = GetComponent<SpaceShip>().playerData;

        manager.ammoCapacity = ammoConfig.defaultCapacity;
        manager.ammoLeft = manager.ammoCapacity;

        ParticleSystem particle = tool.laserHitParticles;

        particlePool = new ObjectPool<ParticleSystem>(() => //Defines the functions of the Object Pool
        {
            return Instantiate(particle);
        }, particle =>
        {
            particle.gameObject.SetActive(true);
        }, particle =>
        {
            particle.gameObject.SetActive(false);
        }, particle =>
        {
            Destroy(particle.gameObject);
        }, true, 40, 100);
    }

    void Update()
    {
        if (tool.mineType == mineToolType.beam)
        {
            if (held && canFire)
            {
                BeamFire();
            }
            else
            {
                OnBeamRelease();
            }
        }
        else if(tool.mineType == mineToolType.bullet)
        {
            if (held && canFire)
            {
                BulletFire();
            }
        }

        regenAmmo();
    }

    private void BeamFire()
    {
        RaycastHit Hitinfo;


        if (TargetInfo.IsTargetInRange(OriginMiddle.position, OriginMiddle.forward, out Hitinfo, tool.laserRange, tool.shootingMask))
        {
            IShootable target = Hitinfo.transform.GetComponent<IShootable>();

            if (target != null)
            {
                target.damage(tool.miningPower + (1.5f* levels.minePowerLevel));//total power of laser
            }
            //Instantiate(tool.laserHitParticles, Hitinfo.point, Quaternion.LookRotation(Hitinfo.normal));
            
            ParticleSystem particles = particlePool.Get();
            particles.transform.position = Hitinfo.point;
            particles.transform.rotation = Quaternion.LookRotation(Hitinfo.normal);
            StartCoroutine(ReturnParticles(particles));
            
            

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
        DecrementAmmo();
    }

    public void OnBeamRelease() 
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
            DecrementAmmo();
            lastShootTime = Time.time;
        }
    }

    public void DecrementAmmo()
    {
        lastShootTime = Time.time;
        manager.ammoLeft -= ammoConfig.ammoUsedPerShot;
        if (manager.ammoLeft <= 0)
        {
            canFire = false;
            if (minetool.mineType == mineToolType.beam)
            {
                OnBeamRelease(); //force inturrupt beam
            }
        }
    }

    public void regenAmmo()
    {

        if (Time.time >= lastShootTime + ammoConfig.regenInterval)
        {
            if (ammoConfig.regenRate != 0) //this is so we can have ammo types that don't regenerate run this code
            {
                manager.ammoLeft += ammoConfig.regenRate;
            }
        }
        if (manager.ammoLeft > manager.ammoCapacity) //clamp max value
        {
            manager.ammoLeft = manager.ammoCapacity;
        }

        if (!canFire)
        {
            if (manager.ammoLeft >= manager.ammoCapacity)
            {
                canFire = true;
            }
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

    IEnumerator ReturnParticles(ParticleSystem ps)
    {
        yield return new WaitForSeconds(1);
        particlePool.Release(ps);
    }
}
