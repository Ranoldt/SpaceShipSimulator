using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectedPart : MonoBehaviour
{
    public TMP_Text display;
    [HideInInspector] public ShipPart shipComponent;

    public List<ShipPart> componentList;

    private void Awake()
    {
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
