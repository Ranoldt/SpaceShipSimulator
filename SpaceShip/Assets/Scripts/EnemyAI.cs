using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    public float movementSpeed = 10f;
    [SerializeField]
    public float rotationalDamp = 1f;
    private Rigidbody rb;
    [SerializeField]
    public Transform player;

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

    bool canFire = true;

    [SerializeField]
    private EnemyShip enemyShip = EnemyShip.chase;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyShip)
        {
            case EnemyShip.chase:
                Chase();
                if ((transform.position - player.position).magnitude <= 10)
                    enemyShip = EnemyShip.attack;
                break;
            case EnemyShip.attack:
                if (InFront() && HaveLineOfSight())
                    Attack();
                if ((transform.position - player.position).magnitude > 10)
                    enemyShip = EnemyShip.chase;
                    break;

        }
      
    }

    void Chase()
    {
        Vector3 pos = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    void Attack()
    {
        foreach (var laser in EnemyLaser)
        {
            if (canFire)
            {
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

        if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
        {
            return true;
        }
        return false;
    }

    bool HaveLineOfSight()
    {


        Vector3 direction = target.position - transform.position;

        if (Physics.Raycast(Middle.transform.position, direction, out hit, LaserRange))
        {
            if (hit.transform.CompareTag("Player"))
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
     
    public enum EnemyShip
    {
        chase,
        attack
    }

}

