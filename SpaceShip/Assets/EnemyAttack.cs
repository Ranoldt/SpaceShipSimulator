using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform Middle;
    [SerializeField]
    private Transform[] LaserOrigin;
    [SerializeField]
    private LineRenderer[] EnemyLaser;

    private float LaserRange = 100f;
    private float LaserOffTime = .5f;
    private float fireDelay = 2f;
    RaycastHit hit;

    bool canFire =true;

    void Update()
    {
        if(InFront() && HaveLineOfSight())
        {
            foreach (var laser in EnemyLaser)
            {
                if(canFire)
                {
                    Vector3 localHitPosition = laser.transform.InverseTransformPoint(hit.point);
                    laser.SetPosition(1, localHitPosition);
                    laser.gameObject.SetActive(true);
                    canFire = false;
                    Invoke("TurnOffLaser", LaserOffTime);
                    Invoke("CanFire", fireDelay);
                }
            }
        }
    }
    bool InFront()
    {
        Vector3 directionToTarget = transform.position - target.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        if(Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
        {
            return true;
        }
        return false;
    }

    bool HaveLineOfSight()
    {
       

        Vector3 direction = target.position - transform.position;

        if(Physics.Raycast(Middle.transform.position, direction, out hit, LaserRange))
        {
            if(hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    void TurnOffLaser()
    {
        foreach (var laser in EnemyLaser)
        laser.gameObject.SetActive(false);
    }

    void CanFire()
    {
        canFire = true;
    }
}
