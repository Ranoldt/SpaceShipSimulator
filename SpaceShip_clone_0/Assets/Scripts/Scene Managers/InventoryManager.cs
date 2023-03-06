using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// CREATED BY GAME MANAGER, USED BY PLAYER PREFAB AND UI
/// 
/// responsible for holding the player's inventory contents
/// also responsible for holding the components that the player has bought,
/// as well as the current level of that item
/// </summary>
public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private PlayerManager playerManager;
    private void Awake()
    {
      int ID = playerManager.playerID;
      equippedMineTool = GameManager.instance.playerShips[ID].tool;
      equippedBoost = GameManager.instance.playerShips[ID].boost;
    }
    public List<InventorySlot> Container = new List<InventorySlot>();

    //inventory of components you have access to
    public List<MineObjects> miningToolContainer = new List<MineObjects>();

    public List<BoostComponent> boostContainer = new List<BoostComponent>();

    //public so other scripts can change this
    public MineObjects equippedMineTool;
    public BoostComponent equippedBoost;


    public int currentCash;

    //event when the player's ore inventory needs to be updated
    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    //event when you buy a new mining tool
    public delegate void OnMineToolBought();
    public OnMineToolBought OnMineToolBoughtCallback;

    //event when you buy a new boost
    public delegate void OnBoostBought();
    public OnBoostBought OnBoostBoughtCallback;

    //the current inventory size (increases per upgrade)
    public float InvSize;

    public bool addItem(ItemObject _item, int _amount)
    {
        //check if you have item in inv
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item && Container[i].amount < _item.MaxStack)
            {
                Container[i].addAmount(_amount);
                hasItem = true;
                if (OnItemChangedCallback != null)
                    OnItemChangedCallback.Invoke();
                return true;
            }
        }

        if (!hasItem && Container.Count < InvSize) //add a new slot when you have available space
        {
            Container.Add(new InventorySlot(_item, _amount));

            if (OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke();
            return true;
        }

        return false;
    }

    public void sellItems() //sells selected items
    {
        for (int i = Container.Count - 1; i >= 0; i--)
        {
            if (Container[i].selected)
            {
                Debug.Log("selling"+ Container[i].item.name);
                int stackPrice = Container[i].item.SellAmount * Container[i].amount;
                currentCash += stackPrice;
                Container.RemoveAt(i);
            }

        }
        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke(); //tells inventory UI to update

    }


    public void selectItem(int ind)
    {
        if(ind <= Container.Count-1)
        {
            Container[ind].selected = !Container[ind].selected;
        }
    }

}

//used for ores, anything sellable
[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;
    public bool selected;

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
