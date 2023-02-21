using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MineObjects : ScriptableObject
{
    public LayerMask shootingMask;

    public float laserRange = 100f;

    public ParticleSystem laserHitParticles;

    public float miningPower = 1f;

    public float laserHeatThreshold = 2f;

    public float laserHeatRate = 0.25f;

    public float laserCoolRate = 0.5f;

    public FloatVariable minepowerlevel;
}
