using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObject : ScriptableObject
{
    public float spawnerRadius;

    public int maxEntities;

    [System.Serializable]
    public class Aster
    {
        public GameObject _asteroid;
        public float dropChance;
    }


    public Aster[] AsterTable;


}
