using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour, IShootable
{
    [Tooltip("\"Fractured\" is the object that this will break into")]
    public GameObject fractured;

    [SerializeField]
    private int itemDropRadius;

    [SerializeField]
    private Asteroid AsteroidData;
    //goal: keep track of health and deplete it when it is shot.
    //when it dies, it plays an explosion effect and 
    float currentHealth;
    int amountToSpawn;
    private float randomization;


    private void Awake()
    {
        currentHealth = AsteroidData.health;
    }
    public void damage(float hit)
    {
        currentHealth -= hit;
        OnDeath();
    }

    private void OnDeath()
    {
        if(currentHealth <= 0)
        {
            CalculateDrops();
            FractureObject();
        }
    }

    private void CalculateDrops() //based on the info in the lootable list, calculate if the drops will happen, and how many to drop
    {
        for(int i = 0; i< AsteroidData.lootable.Length; i++)
        {
            randomization = UnityEngine.Random.Range(0f, 1f);


            if (randomization <= AsteroidData.lootable[i].dropChance)
            {
                amountToSpawn = UnityEngine.Random.Range(AsteroidData.lootable[i].minAmount, AsteroidData.lootable[i].maxAmount);

                for (int j = 0; j <= amountToSpawn; j++)
                {
                    SpawnDrops(AsteroidData.lootable[i].item);
                }
            }
        }

    }

    private void SpawnDrops(GameObject item) //spawn items at randomized location within radius
    {
        var RandomPoint = UnityEngine.Random.insideUnitSphere * itemDropRadius + this.transform.position;

        Instantiate(item, RandomPoint, Quaternion.identity);

    }

    public void FractureObject()
    {
        Instantiate(fractured, transform.position, transform.rotation); //Spawn in the broken version
        Destroy(gameObject); //Destroy the object to stop it getting in the way
    }
}
