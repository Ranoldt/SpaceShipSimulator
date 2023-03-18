using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    [SerializeField]
    Transform[] targets;
    [SerializeField]
    public float movementSpeed = 10f;
    [SerializeField]
    public float rotationalDamp = 1f;
    [SerializeField]
    private Transform Middle;

    private float LaserRange = 100f;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool InFront(int targetIndex)
    {
        Transform target = targets[targetIndex];
            Vector3 directionToTarget = transform.position - target.position;
            float angle = Vector3.Angle(transform.forward, directionToTarget);


            if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
            {
                return true;
            }
        return false;
    }

    bool HaveLineOfSight(string tag, int targetIndex)
    {
        Transform target = targets[targetIndex];
        Vector3 direction = target.position - transform.position;

        if (Physics.Raycast(Middle.transform.position, direction, out hit, LaserRange))
        {
            if (hit.transform.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    void Chase(int targetIndex)
    {
        Transform target = targets[targetIndex];
            Vector3 pos = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(pos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    void Wander()
    {

    }
    public enum playerAI
    {
        wander,
        attack,
        collect,
        mine

    }
  
}
