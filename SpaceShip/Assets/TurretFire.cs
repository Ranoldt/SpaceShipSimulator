using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFire : MonoBehaviour
{
    [SerializeField]
    private Transform Head,barrel;
    [SerializeField]
    private Transform Target;
    public GameObject projectile;

    public float howClose;
    public float fireRate, nextFire;
    private float dist;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(Target.position, transform.position);
        if(dist <= howClose)
        {
            Head.LookAt(Target);
            if(Time.time >= nextFire)
            {
                nextFire = Time.time + 1f / fireRate;
                Shoot();

            }

        }
    }
    
    void Shoot()
    {
        GameObject clone = Instantiate(projectile, barrel.position, Head.rotation);
        clone.transform.Rotate(90, 0, 0);
        clone.GetComponent<Rigidbody>().AddForce(Head.forward * 1000);
        Destroy(clone, 10);
    }

}
