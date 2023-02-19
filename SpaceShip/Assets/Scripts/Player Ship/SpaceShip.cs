using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(Rigidbody))]
public class SpaceShip : MonoBehaviour
{

    [SerializeField]
    private ShipObject shipdata;


    Rigidbody rb;

    private float thrustB;
    private float upDownB;
    private float strafeB;
    private float rollB;
    private Vector2 pitchYaw;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
       //Roll
        rb.AddRelativeTorque(Vector3.back * rollB * shipdata.rollTorque * Time.deltaTime);
        //pitch
        rb.AddRelativeTorque(Vector3.right * Mathf.Clamp(-pitchYaw.y,-1f,1f) * shipdata.pitchTorque * Time.deltaTime);
        //Yaw
        rb.AddRelativeTorque(Vector3.up * Mathf.Clamp(pitchYaw.x,-1f,1f) * shipdata.yawTorque * Time.deltaTime);
       

        //Thurst
        if (thrustB > 0.1f || thrustB < -0.1f)
        {
            float currentThrust = shipdata.thrust;
            
            rb.AddRelativeForce(Vector3.forward * thrustB * currentThrust * Time.deltaTime);

        }
        

        //UpDown
        if (upDownB > 0.1f || upDownB < -0.1f)
        {
            rb.AddRelativeForce(Vector3.up * upDownB * shipdata.upThrust * Time.fixedDeltaTime);
            
        }
        

        //Strafing
        if(strafeB >0.1f || strafeB <-0.1f)
        {
            rb.AddRelativeTorque(Vector3.up * strafeB * shipdata.strafeThrust * Time.fixedDeltaTime);
            
        }
        
    }
   
    public void OnThurst(InputAction.CallbackContext context)
    {
        thrustB = context.ReadValue<float>();
    }

    public void OnUpDown(InputAction.CallbackContext context)
    {
        upDownB = context.ReadValue<float>();
    }

    public void OnStrafe(InputAction.CallbackContext context)
    {
        strafeB = context.ReadValue<float>();

    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        rollB = context.ReadValue<float>();
    }

    public void OnPitchYaw(InputAction.CallbackContext context)
    {
        pitchYaw = context.ReadValue<Vector2>();
    }


}
