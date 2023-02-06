using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionGoAway : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    // Update is called once per frame
    void Update()
    {
        if(animator.GetBool("Destroy") == true)
        {
            Destroy(gameObject);
        }
    }
}
