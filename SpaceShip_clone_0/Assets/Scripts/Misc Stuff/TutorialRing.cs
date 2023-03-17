using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialRing : MonoBehaviour
{

    [SerializeField]
    private UnityEvent response;

    private void OnTriggerEnter(Collider other)
    {
        response.Invoke();
    }
}
