using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Boost : MonoBehaviour
{
    public BoostComponent boostdata;

    [SerializeField]
    private Slider boostBar;

    private bool boost;
    private float boostLeft;
    private Rigidbody rb;

    private float lastBoostTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        boostLeft = boostdata.boostTime;
        boostBar.value = 1;
    }

    private void FixedUpdate()
    {
        BoostMove();
        boostRecovery();
        UpdateUI();
    }

    private void boostRecovery()
    {
        if (Time.time >= lastBoostTime + boostdata.boostRecoveryInterval && !boost)
        {
            boostLeft += (Time.deltaTime * boostdata.boostRecoveryRate);
            boostLeft = Mathf.Clamp(boostLeft, 0, boostdata.boostTime);
        }
    }

    private void UpdateUI()
    {
        boostBar.value = boostLeft / boostdata.boostTime; //value made as a percentage so you dont have to adjust the values every time boostTime changes
    }

    private void BoostMove()
    {
        if(boost && boostLeft > 0)
        {
            rb.AddRelativeForce(Vector3.forward * boostdata.boostStrength * Time.deltaTime);
            boostLeft -= Time.fixedDeltaTime;
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
