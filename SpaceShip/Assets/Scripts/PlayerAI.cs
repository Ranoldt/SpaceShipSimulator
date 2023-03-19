using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    [SerializeField]
    private playerAI AIplayer;
    [SerializeField]
    Transform[] targets;
    [SerializeField]
    public float movementSpeed = 10f;
    [SerializeField]
    public float rotationalDamp = 1f;
    [SerializeField]
    private Transform Middle;
    [SerializeField]
    int Range = 10;
    int height = 1;
    [SerializeField]
    private LevelUp levels;
    [SerializeField]
    private LineRenderer[] Lasers;
    [SerializeField]
    private Transform[] LaserOrigin;
    private MineObjects tool { get { return GetComponent<MineObjects>(); } }
    public InventoryManager inv;
    //public InventorySlot instance;

    bool canFire = true;
    private float LaserOffTime = .5f;
    private float fireDelay = 2f;
    Vector3 wayPoint;

    private float SightRange = 30;
    private float health;
    [SerializeField]
    private float power;
    RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        Wander();
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (AIplayer)
        {
            case playerAI.wander:
                
                    Chase(wayPoint);
                    if ((transform.position - wayPoint).magnitude < 3)
                        Wander();

                if (HaveLineOfSight("Asteroids"))
                    AIplayer = playerAI.mine;
                //if ((transform.position - targets[1].position).magnitude < 10 && (transform.position - targets[2].position).magnitude < 10)
                    //AIplayer = playerAI.attack;

                if (inv.Container.Count > 0)
                    AIplayer = playerAI.sell;

                break;
            case playerAI.collect:
                GameObject[] CollectTargets = GameObject.FindGameObjectsWithTag("Collects");
                for (int i = 0; i < CollectTargets.Length; i++)
                {
                    if (HaveLineOfSight("Collects") && InFront("Collects"))
                    {
                        Chase(CollectTargets[i].transform.position);
                        //inv.addItem(instance.item, instance.amount);
                        break;
                    }
                    else
                        AIplayer = playerAI.wander;
                }
                break;
            case playerAI.mine:
                if (HaveLineOfSight("Asteroids") && InFront("Asteroids"))
                    Mine();
                if (HaveLineOfSight("Collects"))
                    AIplayer = playerAI.collect;
                break;
            case playerAI.attack:
                
                float previousHealth = health;

                if (health < previousHealth)
                {
                    Attack();
                }
                if ((transform.position-targets[1].position).magnitude > 30 && (transform.position - targets[2].position).magnitude > 30)
                    AIplayer = playerAI.wander;
                break;
            case playerAI.sell:
                int spaceLeft = (int)inv.InvSize - inv.Container.Count;
                if (spaceLeft <= 0)
                {
                    Sell();
                }
                else
                    AIplayer = playerAI.wander;
                    break;
            case playerAI.die:
                Destroy(this.gameObject);
                break;
        }
    }

    public void damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            AIplayer = playerAI.die;
        }
    }

    bool InFront(string tag)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject target in targets)
        {
            Vector3 directionToTarget = transform.position - target.transform.position;
            float angle = Vector3.Angle(transform.forward, directionToTarget);


            if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
            {
                return true;
            }
        }
        return false;
    }

    bool HaveLineOfSight(string tag)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject target in targets)
        {
            Vector3 direction = target.transform.position - transform.position;

            if (Physics.Raycast(Middle.transform.position, direction, out hit, SightRange))
            {
                if (hit.transform.CompareTag(tag))
                {
                    return true;
                }
            }
        }
        return false;
    }

    void Chase(Vector3 destination)
    {
        Vector3 pos = destination - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    void Wander()
    {
        wayPoint = new Vector3(Random.Range(transform.position.x - Range, transform.position.x + Range), Random.Range(transform.position.y - height, transform.position.y + height), Random.Range(transform.position.z - Range, transform.position.z + Range));
        transform.LookAt(wayPoint);
    }

    void Mine()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroids");
        if (asteroids.Length > 0)
        {
            GameObject closestAsteroid = asteroids[0];
            float closestDistance = Vector3.Distance(transform.position, closestAsteroid.transform.position);
            foreach (GameObject asteroid in asteroids)
            {
                float distance = Vector3.Distance(transform.position, asteroid.transform.position);
                if (distance < closestDistance)
                {
                    closestAsteroid = asteroid;
                    closestDistance = distance;
                }
            }

            Vector3 direction = closestAsteroid.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);

            foreach (var laser in Lasers)
            {
                RaycastHit hit;
                if (Physics.Raycast(Middle.transform.position, Middle.transform.forward, out hit))
                {
                    if (hit.collider.gameObject.tag == "Asteroids" && hit.collider.gameObject == closestAsteroid)
                    {
                        Vector3 localHitPosition = laser.transform.InverseTransformPoint(hit.point);
                        laser.SetPosition(1, localHitPosition);
                        laser.gameObject.SetActive(true);
                    }
                }
            }
        }

    }

    void Attack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        // Chase and attack the closest enemy if it exists and is within range
        if (closestEnemy != null && closestDistance <= 30f)
        {
            Chase(closestEnemy.transform.position);
            if (HaveLineOfSight("Enemy") && InFront("Enemy"))
            {
                foreach (var laser in Lasers)
                {
                    if (canFire)
                    {
                        {
                            Vector3 localHitPosition = laser.transform.InverseTransformPoint(hit.point);
                            laser.SetPosition(1, localHitPosition);
                            laser.gameObject.SetActive(true);
                            IShootable enemyDmg = closestEnemy.GetComponent<IShootable>();
                            if (enemyDmg != null)
                            {
                                enemyDmg.damage(power);
                            }
                            canFire = false;
                            Invoke("TurnOffLaser", LaserOffTime);
                            Invoke("CanFire", fireDelay);
                        }
                    }
                }
            }
        }
    }


    void Sell()
    {
        Chase(targets[0].position);
        for (int i = inv.Container.Count - 1; i >= 0; i--)
        {
            int stackPrice = inv.Container[i].item.SellAmount * inv.Container[i].amount;
            inv.currentCash += stackPrice;
            inv.Container.RemoveAt(i);

            if (inv.Container.Count < inv.InvSize)
            {
                break;
            }
        }
    }

        void TurnOffLaser()
        {
            foreach (var laser in Lasers)
                laser.gameObject.SetActive(false);
        }

        void CanFire()
        {
            canFire = true;
        }
    }
    public enum playerAI
    {
        wander,
        attack,
        collect,
        mine,
        sell,
        die

    }

  

