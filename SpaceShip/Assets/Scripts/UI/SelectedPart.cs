using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectedPart : MonoBehaviour
{
    public TMP_Text display; //text on ui
    [HideInInspector] public ShipPart shipComponent;

    public List<ShipPart> componentList { get { return _componentList; } private set { _componentList = value; } }
    [SerializeField]
    private List<ShipPart> _componentList;

    private void Awake()
    {
        //set default values
        shipComponent = componentList[0];
        display.text = shipComponent.name;
    }

    public void onLeftButtonClick()
    {
        var index = componentList.IndexOf(shipComponent);

        if (index == 0)
            index = componentList.Count-1;
        else
            index -= 1;

        shipComponent = componentList[index];
        display.text = shipComponent.name;
    }

    public void onRightButtonClick()
    {
        var index = componentList.IndexOf(shipComponent);

        if (index == componentList.Count - 1)
            index = 0;
        else
            index += 1;

        shipComponent = componentList[index];
        display.text = shipComponent.name;
    }
}
