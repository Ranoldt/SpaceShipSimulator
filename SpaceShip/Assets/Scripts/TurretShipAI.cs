using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
//RONALD

public class TurretShipAI : MonoBehaviour, IShootable
{
    private float health;
    [SerializeField]
    float Speed = 20;
    [SerializeField]
    int Range = 10;
    int height = 1;

    Vector3 wayPoint;

    [SerializeField]
    private Transform Head, barrel;
    [SerializeField]
    private Transform Target;
    public GameObject projectile;

    public float howClose;
    public float fireRate, nextFire;
    private float dist;

    [SerializeField]
    private TurretShip turretShip = TurretShip.wander;

    IObjectPool<GameObject> projPool;
    [SerializeField]
    private EnemyProjectile proj;
    // Start is called before the first frame update
    void Start()
    {
        Wander();
        health = 25;

        projPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(projectile);
        }, projectile =>
        {
            projectile.gameObject.SetActive(true);
        }, projectile =>
        {
            projectile.gameObject.SetActive(false);
        }, projectile =>
        {
            Destroy(projectile.gameObject);
        }, true, 15, 30);
    }
    private void OnEnable()
    {
        //set target player randomly if splitscreen
        //if not, then target one player all the time
        int playertoTarget;
        var players = GameManager.instance.players;
        if (players.Count == 2)
            playertoTarget = Random.Range(0, 2);//either 0 or 1
        else
        {
            playertoTarget = 0;
        }
        Target = players[playertoTarget].transform;
    }

    public void damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            turretShip = TurretShip.die;
        }
    }


    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(Target.position, transform.position);
        switch (turretShip)
        {
            case TurretShip.wander:
                transform.position += transform.TransformDirection(Vector3.forward) * Speed * Time.deltaTime;
                if ((transform.position - wayPoint).magnitude < 3)
                    Wander();
                if (dist <= howClose)
                    turretShip = TurretShip.shoot;
                break;
            case TurretShip.shoot:
                    Head.LookAt(Target);
                    Shoot();
                if (dist > howClose)
                    turretShip = TurretShip.wander;
                break;
            case TurretShip.die:
                Destroy(this.gameObject);
                break;
        }
       
    }

    void Wander()
    {
        
        wayPoint = new Vector3(Random.Range(transform.position.x - Range, transform.position.x + Range), Random.Range(transform.position.y - height, transform.position.y + height), Random.Range(transform.position.z - Range, transform.position.z + Range));
        transform.LookAt(wayPoint);

    }

    void Shoot()
    {
        if (Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            //GameObject clone = Instantiate(projectile, barrel.position, Head.rotation);
            GameObject clone = projPool.Get();
            clone.transform.position = barrel.position;
            clone.transform.rotation = Head.rotation;
            clone.transform.Rotate(90, 0, 0);
            clone.GetComponent<Rigidbody>().AddForce(Head.forward * 1000);
            //Destroy(clone, 10);
            StartCoroutine(ReturnToPool(clone));
        }
    }

    public enum TurretShip
    {
        wander,
        shoot,
        die
    }

    IEnumerator ReturnToPool(GameObject _proj)
    {
        yield return new WaitForSeconds(10);
        projPool.Release(_proj);
    }
    
}
