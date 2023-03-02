using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CREATED BY GAME MANAGER, USED BY PLAYER PREFAB AND UI
/// 
/// This holds key player variables that are accessed by UI and player scripts
/// there is one instance of the player manager object per player that is created on initialization
/// > as it is created, the player and ui prefabs of each player stores a reference to it
///   so that they know which manager is theirs
/// > because these attributes frequently change (boost, health, ammo, etc), they are public.
/// </summary>
public class PlayerManager : MonoBehaviour
{
    //variables for boosting
    public float boostLeft;
    public float boostCapacity; //updated whenever boost capacity levels up, or if you buy a new component

    //variables for mining tools
    public float ammoLeft;
    public float ammoCapacity; //updated whenever ammo capacity increases, or if you buy a new component


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
