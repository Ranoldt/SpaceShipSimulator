using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//UNUSED

//[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
//    //different inventories to keep track of
//    //inventory in the world
//    public List<InventorySlot> Container { get { return container; } private set { container = value; } }
    
//    [SerializeField]
//    private List<InventorySlot> container = new List<InventorySlot>();

//    //inventory of components you have access to
//    public List<MineObjects> miningToolContainer { get { return _miningToolContainer; } private set { _miningToolContainer = value; } }
//    [SerializeField]
//    private List<MineObjects> _miningToolContainer = new List<MineObjects>();

//    public List<BoostComponent> boostContainer { get { return _boostContainer; } private set { _boostContainer = value; } }
//    [SerializeField]
//    private List<BoostComponent> _boostContainer = new List<BoostComponent>();


//    public FloatVariable currentCash;

//    //event when the player's ore inventory needs to be updated
//    public delegate void OnItemChanged();
//    public OnItemChanged OnItemChangedCallback;

//    //event when you buy a new mining tool
//    public delegate void OnMineToolBought();
//    public OnMineToolBought OnMineToolBoughtCallback;

//    //event when you buy a new boost
//    public delegate void OnBoostBought();
//    public OnBoostBought OnBoostBoughtCallback;

//    public int MinSize { get {return minsize; } private set { minsize = value; } }

//    //default size of the inventory
//    [SerializeField]
//    private int minsize;

//    //the current inventory size (increases per upgrade)
//    public FloatVariable InvSize;

//    public bool addItem(ItemObject _item, int _amount)
//    {
//        //check if you have item in inv
//        bool hasItem = false;
//        for (int i = 0; i < Container.Count; i ++)
//        {
//            if(Container[i].item == _item && Container[i].amount < _item.MaxStack)
//            {
//                Container[i].addAmount(_amount);
//                hasItem = true;
//                if (OnItemChangedCallback != null)
//                    OnItemChangedCallback.Invoke();
//                return true;
//            }
//        }

//        if (!hasItem && Container.Count < InvSize.FloatValue) //add a new slot when you have available space
//        {
//            Container.Add(new InventorySlot(_item, _amount));

//            if(OnItemChangedCallback != null)
//                OnItemChangedCallback.Invoke();
//            return true;
//        }

//        return false;
//    }

//    public void sellItems()
//    {
//        for (int i = Container.Count - 1; i >= 0; i--)
//        {
//            int stackPrice = Container[i].item.SellAmount * Container[i].amount;
//            currentCash.IncrementValue (stackPrice);
//            Container.RemoveAt(i);

//        }

//        if (OnItemChangedCallback != null)
//            OnItemChangedCallback.Invoke(); //tells inventory to update

//    }

//}
////used for ores, anything sellable
//[System.Serializable]
//public class InventorySlot
//{
//    public ItemObject item;
//    public int amount;

//    public InventorySlot(ItemObject _item, int _amount)
//    {
//        item = _item;
//        amount = _amount;
//    }

//    public void addAmount(int value)
//    {
//        amount += value;
//    }
}


