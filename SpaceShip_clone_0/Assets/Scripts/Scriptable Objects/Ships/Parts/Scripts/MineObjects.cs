using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum mineToolType
{
    beam,
    bullet
}

[CreateAssetMenu(fileName = "New Mine Object", menuName = "Agents/Parts/Mine/Tool")]
public class MineObjects : ShipPart
{
    public GameObject fab { get { return _fab; } private set { _fab = value; } }
    [SerializeField]
    private GameObject _fab;
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

    public int powerLevelCapacity { get { return _powerLevelCapacity; } private set { _powerLevelCapacity = value; } }
    [SerializeField]
    private int _powerLevelCapacity;

    public mineToolType mineType;

}
