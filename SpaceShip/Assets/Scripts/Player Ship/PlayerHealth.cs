using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour, IShootable
{
    [SerializeField]
    private PlayerManager player;
    [SerializeField]
    private float startingHealth;
    [SerializeField]
    private float recoveryInterval;
    [SerializeField]
    private float recoveryRate;

    //death variables
    [SerializeField]
    private GameObject deathEffect;
    [SerializeField]
    private float deathTimer;
    private float lastDamageTime;
    

    private void Start()
    {
        player.maxHealth = startingHealth;
        player.playerHealth = player.maxHealth;
    }

    private void Update()
    {
        if(Time.time >= lastDamageTime + recoveryInterval)
        {
            healthRecovery();
        }
    }

    public void damage(float damage)
    {
        player.playerHealth -= damage;
        lastDamageTime = Time.time;

        if(player.playerHealth <= 0)
        {
           StartCoroutine(death());
        }
    }

    private void healthRecovery()
    {
        player.playerHealth += recoveryRate;

        if(player.playerHealth >= player.maxHealth)
        {
            player.playerHealth = player.maxHealth;
        }
    }

    private IEnumerator death()
    {
        this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        Instantiate(deathEffect, this.transform.position, this.transform.rotation);
        var input = this.gameObject.GetComponent<PlayerInput>();
        input.DeactivateInput();


        yield return new WaitForSeconds(deathTimer);
        this.transform.root.position = (Vector3.zero);
        this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        input.ActivateInput();

    }
}
