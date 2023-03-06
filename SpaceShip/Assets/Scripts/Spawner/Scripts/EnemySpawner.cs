using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private int maxEntities;

    [SerializeField]
    private float spawnRadius;

    [SerializeField]
    private List<GameObject> enemies;


    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.players.Count != 0 && this.transform.childCount < maxEntities)
        {
            var objToCheck = enemies[Random.Range(0, enemies.Count)];
            GameObject enemy = Instantiate(objToCheck, GetCoords(), Quaternion.identity);
            enemy.transform.parent = this.transform;
        }
    }

    private Vector3 GetCoords()
    {
        Vector3 RandomPoint = Random.insideUnitSphere * spawnRadius;
        return RandomPoint;
    }
}
