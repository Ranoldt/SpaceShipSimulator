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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        Move();
    }

    void Turn()
    {
        Vector3 pos = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
    }

    void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }
}
