using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawner", menuName = "World/Spawner")]
public class AsteroidSpawner : SpawnerObject
{
    private Aster objToCheck;
    private float checker;
    public GameObject GetAsteroid()
    {//picks an asteroid object by random, then checks if it should be spawned
        objToCheck = AsterTable[Random.Range(0, AsterTable.Length - 1)];

        checker = Random.Range(0f, 1f);

        if (checker <= objToCheck.dropChance) //spawns the object in a random point
        {
            return objToCheck._asteroid;
        }
        else
        {
            return null;
        }
    }

    public Vector3 GetCoords()
    {
        Vector3 RandomPoint = Random.insideUnitSphere * spawnerRadius;
        return RandomPoint;
    }




}
