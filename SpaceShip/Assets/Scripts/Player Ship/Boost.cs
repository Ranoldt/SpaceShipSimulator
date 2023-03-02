using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Boost : MonoBehaviour
{
    [SerializeField]
    private Event boostInitializationEvent;
    //raised on start, as well as whenever boost components are equipped

    private PlayerManager manager;

    //Behavior for the act of boosting
    private BoostComponent boostdata;

    private float lastBoostTime;
    private bool isBoost;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        manager = GetComponent<SpaceShip>().playerData;
        boostInitializationEvent.Raise();
    }

    private void FixedUpdate()
    {
        BoostMove();
        boostRecovery();
    }

    private void boostRecovery()
    {
        if (Time.time >= lastBoostTime + boostdata.boostRecoveryInterval && !isBoost)
        {
            manager.boostLeft += Time.deltaTime * boostdata.boostRecoveryRate;
            manager.boostLeft =  Mathf.Clamp(manager.boostLeft, 0, boostdata.boostTime);
        }
    }

    private void BoostMove()
    {
        if(isBoost && manager.boostLeft > 0)
        {
            rb.AddRelativeForce(Vector3.forward * boostdata.boostStrength * Time.deltaTime);
            manager.boostLeft -=  Time.fixedDeltaTime;
        }
    }

    public void onBoost(InputAction.CallbackContext context)
    {
        isBoost = context.performed;

        if (context.canceled)
        {
            isBoost = false;
            lastBoostTime = Time.time;
        }
    }


    public void BoostInitialization()
    {
        //get the equipped component
        boostdata = GetComponent<SpaceShip>().inv.equippedBoost;
        //populate runtime variables
        manager.boostCapacity = boostdata.boostTime;
        manager.boostLeft = manager.boostCapacity;
    }
}
