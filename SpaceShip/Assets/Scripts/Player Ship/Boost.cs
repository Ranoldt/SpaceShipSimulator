using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Boost : MonoBehaviour
{
    [SerializeField]
    private float boostStrength;

    [SerializeField]
    private float boostRecoveryRate;

    [SerializeField]
    private float boostRecoveryInterval;

    [SerializeField]
    private Slider boostBar;

    [SerializeField]
    private float boostTime;

    private bool boost;
    private float boostLeft;
    private Rigidbody rb;

    private float lastBoostTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        boostLeft = boostTime;
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
        if (Time.time >= lastBoostTime + boostRecoveryInterval && !boost)
        {
            boostLeft += (Time.deltaTime * boostRecoveryRate);
            boostLeft = Mathf.Clamp(boostLeft, 0, boostTime);
        }
    }

    private void UpdateUI()
    {
        boostBar.value = boostLeft / boostTime;
    }

    private void BoostMove()
    {
        if(boost && boostLeft > 0)
        {
            Debug.Log("boosting");
            rb.AddRelativeForce(Vector3.forward * boostStrength * Time.deltaTime);
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
