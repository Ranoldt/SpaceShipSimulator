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
    public List<LineRenderer> beams = new List<LineRenderer>();
    private MineObjects tool { get { return GetComponent<SpaceShip>().inv.equippedMineTool; } }
    public InventoryManager inv;


    Vector3 wayPoint;

    private float SightRange = 30;
    private float health;
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
        switch(AIplayer)
        {
            case playerAI.wander:
                Chase(wayPoint);
                if ((transform.position - wayPoint).magnitude < 3)
                    Wander();
                if (HaveLineOfSight("Collects"))
                    AIplayer = playerAI.collect;
                if (HaveLineOfSight("Asteroids"))
                    AIplayer = playerAI.mine;
                break;
            case playerAI.collect:
                GameObject[] CollectTargets = GameObject.FindGameObjectsWithTag("Collects");
                for (int i = 0; i < CollectTargets.Length; i++)
                {
                    if (HaveLineOfSight("Collects") && InFront("Collects"))
                    {
                        Chase(CollectTargets[i].transform.position);
                        break;
                    }
                    else
                        AIplayer = playerAI.wander;
                }
                break;
            case playerAI.mine:
                if (HaveLineOfSight("Asteroids") && InFront("Asteroids"))
                    Mine();
                break;
            case playerAI.attack:
                break;
            case playerAI.sell:
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
             AIplayer= playerAI.die;
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
        foreach(GameObject target in targets)
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
        RaycastHit Hitinfo;

        if (TargetInfo.IsTargetInRange(Middle.position, Middle.forward, out Hitinfo, tool.laserRange, tool.shootingMask))
        {
            Debug.Log("BRUH");
            IShootable target = Hitinfo.transform.GetComponent<IShootable>();
            if (target != null)
            {
                target.damage(tool.miningPower + (1.5f * levels.minePowerLevel));//total power of laser
            }
            Instantiate(tool.laserHitParticles, Hitinfo.point, Quaternion.LookRotation(Hitinfo.normal));

            foreach (var beam in beams)
            {
                Debug.Log("Beam work");
                Vector3 localHitPosition = beam.transform.InverseTransformPoint(Hitinfo.point);
                beam.gameObject.SetActive(true);
                beam.SetPosition(1, localHitPosition);
            }
        }
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
  

