using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ShipBuildUI : MonoBehaviour
{
    [SerializeField]
    private ShipObject ship;
    //will assign the appropriate component to the ship object

    private SelectedPart[] parts;
    //list of parts, populated by analyzing the children of the object that this script is attached to.

    private void Awake()
    {
        parts = gameObject.GetComponentsInChildren<SelectedPart>();
    }
    public void onLeftButtonClick()
    {

    }

    public void onRightButtonClick()
    {

    }

}
