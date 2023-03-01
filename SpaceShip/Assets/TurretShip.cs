using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretShip : MonoBehaviour
{
    [SerializeField]
    float Speed = 20;
    [SerializeField]
    int Range = 10;
    int height = 1;

    Vector3 wayPoint;

    // Start is called before the first frame update
    void Start()
    {
        Wander();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.TransformDirection(Vector3.forward) * Speed * Time.deltaTime;
        if((transform.position - wayPoint).magnitude < 3)
        {
            Wander();
        }    
    }

    void Wander()
    {
        wayPoint = new Vector3(Random.Range(transform.position.x - Range, transform.position.x + Range), Random.Range(transform.position.y - height, transform.position.y + height), Random.Range(transform.position.z - Range, transform.position.z + Range));
        transform.LookAt(wayPoint);

    }
}
