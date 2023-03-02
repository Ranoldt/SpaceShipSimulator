using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum mineToolType
{
    beam,
    bullet
}
public class MineObjects : ShipPart
{
    public MineAmmoConfig mineAmmoConfig { get { return _mineAmmoConfig;} private set { _mineAmmoConfig = value; } }
    [SerializeField]
    private MineAmmoConfig _mineAmmoConfig;

    public LayerMask shootingMask { get { return _shootingMask; } private set { _shootingMask = value; } }
    [SerializeField]
    private LayerMask _shootingMask;

    public float laserRange { get { return _laserRange; } private set { _laserRange = value; } }
    [SerializeField]
    private float _laserRange = 100f;

    public ParticleSystem laserHitParticles { get { return _laserHitParticles; } private set { _laserHitParticles = value; } }
    [SerializeField]
    private ParticleSystem _laserHitParticles;

    public float miningPower { get { return _miningPower; } private set { _miningPower = value; } }
    [SerializeField]
    private float _miningPower = 1f;

    //public float laserHeatThreshold { get { return _laserHeatThreshold; } private set { _laserHeatThreshold = value; } }
    //[SerializeField]
    //private float _laserHeatThreshold = 2f;

    //public float laserHeatRate { get { return _laserHeatRate; } private set { _laserHeatRate = value; } }
    //[SerializeField] 
    //private float _laserHeatRate = 0.25f;

    //public float laserCoolRate { get { return _laserCoolRate; } private set { _laserCoolRate = value; } }
    //[SerializeField]
    //private float _laserCoolRate = 0.5f;

    public FloatVariable minepowerlevel;

    public mineToolType mineType;

    public void _Shoot(Transform[] origin, Transform aimDirection)
    {
        if(this is BeamType)
        {
            (this as BeamType).Shoot(origin, aimDirection);
        }
        if (this is BulletType)
        {
            (this as BulletType).Shoot(origin, aimDirection);
        }
    }
    public void _OnShootRelease()
    {
        if (this is BeamType)
        {
            (this as BeamType).OnShootRelease();
        }
        if (this is BulletType)
        {
            (this as BulletType).OnShootRelease();
        }
    }

}
