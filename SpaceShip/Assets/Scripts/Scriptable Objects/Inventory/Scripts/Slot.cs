using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    //accessing the image components of the inventory slots
    [SerializeField]
    private Image _icon;
    public Image icon { get { return _icon; } private set { icon = value; } }

    [SerializeField]
    private TMP_Text text;

    ItemObject item;

    public void AddItem(ItemObject obj, int value)
    {
        item = obj;
        text.text = value.ToString();
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        text.text = 0.ToString();

        icon.sprite = null;
        //icon.enabled = false;
    }

}
