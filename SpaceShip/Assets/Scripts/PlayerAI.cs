using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    public playerAI AIplayer;

    private bool isReachWaypoint; //wander variable


    [SerializeField]
    Transform[] targets;
    [SerializeField]
    public float movementSpeed = 10f;
    [SerializeField]
    public float rotationalDamp;
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

    public InventoryManager inv;
    //public InventorySlot instance;

    private float sphereRadius = 30f;
    [SerializeField]private List<GameObject> enemiesList = new List<GameObject>();
    [SerializeField]private List<GameObject> mineableList = new List<GameObject>();
    bool canFire = true;
    private float LaserOffTime = .5f;
    private float fireDelay = 2f;
    Vector3 wayPoint;

    private float SightRange = 30;
    [SerializeField]
    private float power;
    RaycastHit hit;

    GameObject[] CollectTargets;

    // Start is called before the first frame update
    void Start()
    {

        wayPoint = new Vector3(Random.Range(transform.position.x - Range, transform.position.x + Range), Random.Range(transform.position.y - height, transform.position.y + height), Random.Range(transform.position.z - Range, transform.position.z + Range));

        AIplayer = playerAI.wander;

        Collider[] Colliders = Physics.OverlapSphere(transform.position, sphereRadius);
        foreach (Collider collider in Colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                enemiesList.Add(collider.gameObject);
            }
            else if (collider.CompareTag("Asteroids"))
            {
                mineableList.Add(collider.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateList();

        switch (AIplayer)
        {
            case playerAI.wander:
                //wander logic

                Chase(wayPoint);
                

                if (Vector3.Distance(this.transform.position, wayPoint) <= 0.5 || wayPoint == Vector3.zero)
                {
                    wayPoint = new Vector3(Random.Range(transform.position.x - Range, transform.position.x + Range), Random.Range(transform.position.y - height, transform.position.y + height), Random.Range(transform.position.z - Range, transform.position.z + Range));
                }

                //wander transitions

                if (mineableList.Count > 0)
                {
                    wayPoint = Vector3.zero;
                    AIplayer = playerAI.mine;
                }

                if (enemiesList.Count != 0)
                {
                    foreach (GameObject enemy in enemiesList)
                        if (Vector3.Distance(transform.position, enemy.transform.position) < 10)
                        {
                            wayPoint = Vector3.zero;
                            AIplayer = playerAI.attack;
                        }
                }

                if (inv.Container.Count > 0)
                {
                    wayPoint = Vector3.zero;
                    AIplayer = playerAI.sell;
                }

                break;

            case playerAI.collect:
                //collect logic
                transform.position += transform.forward * movementSpeed * Time.deltaTime;

                if(!HaveLineOfSight("Collects"))
                {
                    AIplayer = playerAI.wander;
                }


                break;


            case playerAI.mine:
                    Mine();
                if (HaveLineOfSight("Collects"))
                {
                    Invoke("TurnOffLaser", LaserOffTime);
                    AIplayer = playerAI.collect;
                }

                else if(mineableList.Count == 0)
                {
                    AIplayer = playerAI.wander;
                }
                break;


            case playerAI.attack:
                    Attack();
                

                foreach (GameObject enemy in enemiesList)
                    if (Vector3.Distance(transform.position, enemy.transform.position) > 30)
                        AIplayer = playerAI.wander;
                break;

            case playerAI.sell:
                    Sell();

                if(inv.Container.Count == 0)
                    AIplayer = playerAI.wander;
                    break;
            case playerAI.die:
                break;
        }
    }

    void UpdateList()
    {
        enemiesList.Clear();
        mineableList.Clear();

        Collider[] Colliders = Physics.OverlapSphere(transform.position, sphereRadius);
        foreach (Collider collider in Colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                enemiesList.Add(collider.gameObject);
            }
            else if (collider.CompareTag("Asteroids"))
            {
                mineableList.Add(collider.gameObject);
            }
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
        if (targets.Length != 0)
        {
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
        }
        return false;
    }

    void Chase(Vector3 destination)
    {
        Vector3 pos = (destination - transform.position);
        //Quaternion rotation = Quaternion.LookRotation(pos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);

        transform.LookAt(destination);
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }


    void Mine()
    {
        if (mineableList.Count > 0)
        {
            GameObject closestAsteroid = mineableList[0];
            float closestDistance = Vector3.Distance(transform.position, closestAsteroid.transform.position);
            foreach (GameObject asteroid in mineableList)
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
                    IShootable target = hit.transform.GetComponent<IShootable>();

                    if (target != null)
                    {
                        target.damage(power + (1.5f * levels.minePowerLevel));//total power of laser
                    }
                    if (hit.collider.gameObject.CompareTag("Asteroids") && hit.collider.gameObject == closestAsteroid)
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
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        
        foreach (GameObject enemy in enemiesList)
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
        //Debug.Log("going to " + targets[0].position+ ", currently at " + transform.position);
        Chase(targets[0].position);

        if (Vector3.Distance(this.transform.position, targets[0].position) <= 5)
        {
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

  

