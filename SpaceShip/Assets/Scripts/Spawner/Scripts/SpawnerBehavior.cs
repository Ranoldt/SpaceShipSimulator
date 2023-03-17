using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerBehavior : MonoBehaviour
{

    [SerializeField]
    private AsteroidSpawner spawner;
    [SerializeField]
    private Fracture ast;
    public ObjectPool<Fracture> asteroidPool;


    private void Start()
    {
        asteroidPool = new ObjectPool<Fracture>(() =>
        {
            return Instantiate(ast);
        }, ast =>
        {
            ast.gameObject.SetActive(true);
        }, ast =>
        {
            ast.gameObject.SetActive(false);
        }, ast =>
        {
            Destroy(ast.gameObject);
        }, true, 10, 20);
    }
    
    private void Update()
    {
        if(this.transform.childCount < spawner.maxEntities)
        {
            var obj = spawner.GetAsteroid();

            if(obj != null)
            {
                Vector3 coords = spawner.GetCoords();

                
                var child = asteroidPool.Get();
                child.transform.position = coords;
                child.transform.parent = gameObject.transform;
                //var child = Instantiate(obj, coords, Quaternion.identity);
                //child.transform.parent = gameObject.transform;
            }
        }
    }
}
