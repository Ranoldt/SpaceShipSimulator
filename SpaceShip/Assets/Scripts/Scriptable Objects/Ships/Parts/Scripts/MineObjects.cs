using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MineObjects : ShipPart
{
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

    public float laserHeatThreshold { get { return _laserHeatThreshold; } private set { _laserHeatThreshold = value; } }
    [SerializeField]
    private float _laserHeatThreshold = 2f;

    public float laserHeatRate { get { return _laserHeatRate; } private set { _laserHeatRate = value; } }
    [SerializeField] 
    private float _laserHeatRate = 0.25f;

    public float laserCoolRate { get { return _laserCoolRate; } private set { _laserCoolRate = value; } }
    [SerializeField]
    private float _laserCoolRate = 0.5f;

    public FloatVariable minepowerlevel;
}
