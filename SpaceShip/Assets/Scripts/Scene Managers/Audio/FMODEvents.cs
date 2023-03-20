using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
/// <summary>
/// singleton that holds references to fmod events so that we don't have references everywhere in the project
/// </summary>
public class FMODEvents : MonoBehaviour
{

    [field: Header("Bullet Sfx")]
    [field: SerializeField] public EventReference bulletSfx { get; private set; }

    [field: Header("Death Sfx")]
    [field: SerializeField] public EventReference deathSfx { get; private set; }

    [field: Header("Respawn Sfx")]
    [field: SerializeField] public EventReference respawnSfx { get; private set; }

    [field: Header("Player Hurt Sfx")]
    [field: SerializeField] public EventReference hurtSfx { get; private set; }

    [field: Header("Item Pick Up")]
    [field: SerializeField] public EventReference iPickSfx { get; private set; }
    public static FMODEvents instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }


}
