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

    [SerializeField]
    private FloatVariable minePower;
    [SerializeField]
    private FloatVariable laserLevel;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
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
            target.damage(minePower.FloatValue + (1.5f * laserLevel.FloatValue));
            Destroy(this.gameObject);
        }
    }
}
