using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewMineAmmoConfig", menuName = "Agents/Parts/Mine/AmmoConfig", order = 0)]
public class MineAmmoConfig : ScriptableObject
{
    public int defaultCapacity { get{return _defaultCapacity;} private set { _defaultCapacity = value; } }
    [SerializeField]
    private int _defaultCapacity;

    public float regenRate { get{return _regenRate; } private set { _regenRate = value; } }
    [SerializeField]
    private float _regenRate; //if weapons don't regenerate, then set to 0

    public float regenInterval { get { return _regenInterval; } private set { _regenInterval = value; } }
    [SerializeField]
    private float _regenInterval;

    public float ammoUsedPerShot { get { return _ammoUsedPerShot; } private set { _ammoUsedPerShot = value; } }
    [SerializeField]
    private float _ammoUsedPerShot;

    public float shootingInterval { get { return _shootingInterval; } private set { _shootingInterval = value; } }
    [SerializeField]
    private float _shootingInterval;
}




