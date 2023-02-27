using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float life;

    private float timeAlive;

    private Rigidbody rb;

    private float minePower;
    [SerializeField]
    private FloatVariable laserLevel;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        minePower = GetComponentInParent<SpaceShip>().shipdata.miningTool.miningPower;
    }

    private void Update()
    {
        //destroy the bullet if it lives for too long to make sure you don't overload performance
        if(timeAlive >= life)
        {
            Destroy(this.gameObject);
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (transform.up * moveSpeed * Time.fixedDeltaTime));

        timeAlive += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        IShootable target = other.gameObject.transform.GetComponent<IShootable>();

        if(target != null)
        {
            target.damage(minePower * (1.5f * laserLevel.FloatValue));
            Destroy(this.gameObject);
        }
    }
}
