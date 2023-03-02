using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //make items destroy themselves if you dont collect them in time
    [SerializeField]
    private float lifespan;

    private float timeAlive;

    private void Awake()
    {
        timeAlive = 0f;
    }
    private void Update()
    {
        if (timeAlive >= lifespan)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        timeAlive += Time.deltaTime;
    }


    public ItemObject item;
}
