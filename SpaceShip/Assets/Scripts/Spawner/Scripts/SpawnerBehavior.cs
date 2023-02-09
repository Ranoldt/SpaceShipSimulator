using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour
{

    [SerializeField]
    private AsteroidSpawner spawner;

    private void Update()
    {
        if(this.transform.childCount < spawner.maxEntities)
        {
            var obj = spawner.GetAsteroid();

            if(obj != null)
            {
                Vector3 coords = spawner.GetCoords();

                var child = Instantiate(obj, coords, Quaternion.identity);
                child.transform.parent = gameObject.transform;
            }
        }
    }
}
