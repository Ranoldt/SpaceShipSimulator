using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleCamera : MonoBehaviour
{
    public void toggleCam(PlayerInput player)
    {
        this.gameObject.SetActive(false);
    }
}
