using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IShootable shoot = other.gameObject.GetComponent<IShootable>();
            if(shoot != null)
            {
                shoot.damage(damage);
            }
        }
    }
}
