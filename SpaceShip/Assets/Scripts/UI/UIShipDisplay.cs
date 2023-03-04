using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShipDisplay : MonoBehaviour
{
    [SerializeField]
    private MeshFilter shipMeshFilter;

    [SerializeField]
    private ModelSelect meshSelector;
    public void changeMesh()
    {
        shipMeshFilter.mesh = meshSelector.shipComponent.model;
    }
}
