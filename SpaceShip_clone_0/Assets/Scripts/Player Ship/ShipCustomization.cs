using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShipCustomization : MonoBehaviour
{
    /// <summary>
    /// take: prefabs of different ship options available
    /// then, instantiate the appropriate one based on the selection of the ship customization menu
    /// </summary>
    /// 

    [Serializable]
    private class ShipVariants
    {
        [HideInInspector]
        public string variantName;
        public GameObject variantPrefab;
    }

    [SerializeField] [NonReorderable]
    private ShipVariants[] shipVariants = new ShipVariants[System.Enum.GetNames(typeof(mineToolType)).Length];


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
