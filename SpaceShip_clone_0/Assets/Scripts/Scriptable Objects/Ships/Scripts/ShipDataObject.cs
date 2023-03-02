using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDataObject : ScriptableObject
{
    public float yawTorque { get{ return _yawTorque; } private set { _yawTorque = value; } }
    [SerializeField]
    private float _yawTorque;

    public float pitchTorque { get { return _pitchTorque; } private set { _pitchTorque = value; } }
    [SerializeField]
    private float _pitchTorque;

    public float rollTorque { get { return _rollTorque; } private set { _rollTorque = value; } }
    [SerializeField]
    private float _rollTorque;

    public float thrust { get { return _thrust; } private set { _thrust = value; } }
    [SerializeField]
    private float _thrust;

    public float upThrust { get { return _upThrust; } private set { _upThrust = value; } }
    [SerializeField]
    private float _upThrust;

    public float strafeThrust { get { return _strafeThrust; } private set { _strafeThrust = value; } }
    [SerializeField]
    private float _strafeThrust;
}
