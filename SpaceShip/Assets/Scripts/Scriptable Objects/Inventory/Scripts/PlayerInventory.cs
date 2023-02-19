using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private FloatVariable invSize;

    private TMP_Text money;

    public InventoryObject inventory;

    private void Start()
    {
        money = UIManager.UIinstance.moneyText;
    }

    private void Update()
    {
        money.text = "$" + inventory.currentCash.FloatValue.ToString(); //update ui
    }

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();

        if (item)
        {
            other.GetComponent<Collider>().enabled = false; //deal with double counting by making sure on trigger enter only happens once
            inventory.addItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        inventory.currentCash.SetValue(0);
    }
}
