using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplayRotate : MonoBehaviour
{
    [SerializeField]
    private float rotateRate;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(Vector3.forward * rotateRate * Time.deltaTime);
    }
}
