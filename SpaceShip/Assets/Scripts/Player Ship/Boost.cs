using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Boost : MonoBehaviour
{
    //Behavior for the act of boosting
    [SerializeField]
    private BoostComponent boostdata;
    [SerializeField]
    private FloatVariable boostMax;
    private float lastBoostTime;
    private bool boost;

    [SerializeField]
    private FloatVariable BoostCapacity;
    public FloatVariable boostLeft { get { return BoostCapacity; } private set { boostLeft = value; } }
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        boostLeft.SetValue(boostdata.boostTime);
        boostMax.SetValue(boostdata.boostTime);
    }

    private void FixedUpdate()
    {
        BoostMove();
        boostRecovery();
    }

    private void boostRecovery()
    {
        if (Time.time >= lastBoostTime + boostdata.boostRecoveryInterval && !boost)
        {
            boostLeft.IncrementValue (Time.deltaTime * boostdata.boostRecoveryRate);
            boostLeft.SetValue( Mathf.Clamp(boostLeft.FloatValue, 0, boostdata.boostTime));
        }
    }

    private void BoostMove()
    {
        if(boost && boostLeft.FloatValue > 0)
        {
            rb.AddRelativeForce(Vector3.forward * boostdata.boostStrength * Time.deltaTime);
            boostLeft.DecrementValue( Time.fixedDeltaTime);
        }
    }

    public void onBoost(InputAction.CallbackContext context)
    {
        boost = context.performed;

        if (context.canceled)
        {
            boost = false;
            lastBoostTime = Time.time;
        }
    }
}
