using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IShootable
{
    private float health;
    [SerializeField]
    public float movementSpeed = 10f;
    [SerializeField]
    public float rotationalDamp = 1f;
    private Rigidbody rb;

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
    private float power;

    [SerializeField]
    private EnemyShip enemyShip = EnemyShip.chase;

    private void Awake()
    {
        health = 100f;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        int playertoTarget;
        var players = GameManager.instance.players;
        if (players.Count == 2)
            playertoTarget = Random.Range(0, 2);//either 0 or 1
        else
        {
            playertoTarget = 0;
        }
        target = players[playertoTarget].transform;
    }

    public void damage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            enemyShip = EnemyShip.die;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyShip)
        {
            case EnemyShip.chase:
                Chase();
                if ((transform.position - target.position).magnitude <= 10)
                    enemyShip = EnemyShip.attack;
                break;
            case EnemyShip.attack:
                if (InFront() && HaveLineOfSight())
                    Attack();
                if ((transform.position - target.position).magnitude > 10)
                    enemyShip = EnemyShip.chase;
                    break;
            case EnemyShip.die:
                Destroy(this.gameObject);
                break;

        }
      
    }

    void Chase()
    {
        Vector3 pos = target.position - transform.position;
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
                    IShootable playerdmg = target.GetComponent<IShootable>();
                    if(playerdmg != null)
                    {
                        playerdmg.damage(power);
                    }
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
        attack,
        die
    }

}

