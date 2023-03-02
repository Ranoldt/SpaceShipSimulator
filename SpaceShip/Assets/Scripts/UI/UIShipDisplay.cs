using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShipDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject laserShip;

    [SerializeField]
    private GameObject bulletShip;

    private Mesh laserMesh;
    private Mesh bulletMesh;


    private void Start()
    {
        laserMesh = laserShip.GetComponentInChildren<MeshFilter>().sharedMesh;
        bulletMesh = bulletShip.GetComponentInChildren<MeshFilter>().sharedMesh;
    }

    public void changeMesh()
    {
        if (this.gameObject.GetComponent<MeshFilter>().sharedMesh == laserMesh)
        {
            this.gameObject.GetComponent<MeshFilter>().sharedMesh = bulletMesh;
        }
        else
        {
            this.gameObject.GetComponent<MeshFilter>().sharedMesh = laserMesh;
        }
    }
}
