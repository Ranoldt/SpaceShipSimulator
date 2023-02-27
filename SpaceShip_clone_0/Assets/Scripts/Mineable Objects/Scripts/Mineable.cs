using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineable : ScriptableObject
{

    [System.Serializable]
    public class Loots
    {
        public GameObject item;
        public float dropChance;
        public int minAmount;
        public int maxAmount;
    }


    public Loots[] lootable;
    //editable loot chart in the inspector


    public float health;


    public GameObject deathEffect;

}
