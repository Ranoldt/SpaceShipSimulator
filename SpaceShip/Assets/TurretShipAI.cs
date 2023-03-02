using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretShipAI : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        Wander();
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
            GameObject clone = Instantiate(projectile, barrel.position, Head.rotation);
            clone.transform.Rotate(90, 0, 0);
            clone.GetComponent<Rigidbody>().AddForce(Head.forward * 1000);
            Destroy(clone, 10);
        }
    }

    public enum TurretShip
    {
        wander,
        shoot
    }
}
