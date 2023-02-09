using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();

    public int currentCash;

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    public void addItem(ItemObject _item, int _amount)
    {
        //check if you have item in inv
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i ++)
        {
            if(Container[i].item == _item)
            {
                Container[i].addAmount(_amount);
                hasItem = true;
                if (OnItemChangedCallback != null)
                    OnItemChangedCallback.Invoke();
                break;
            }
        }

        if (!hasItem)
        {
            Container.Add(new InventorySlot(_item, _amount));

            if(OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke();
        }
    }

}
[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;

    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void addAmount(int value)
    {
        amount += value;
    }
}

