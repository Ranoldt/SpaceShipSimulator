using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// same as the regular player health, except the death coroutine
/// </summary>
public class AiPlayerHealth : MonoBehaviour, IShootable
{
    [SerializeField] private PlayerAI aiBrain;

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

    [SerializeField]
    private float respawnRadius;

    private bool isDead;

    private void Start()
    {
        player.maxHealth = startingHealth;
        player.playerHealth = player.maxHealth;
    }

    private void Update()
    {
        if (Time.time >= lastDamageTime + recoveryInterval)
        {
            healthRecovery();
        }
    }

    public void damage(float damage)
    {
        player.playerHealth -= damage;
        //play hurt sound
        AudioManager.instance.PlayOneShot(FMODEvents.instance.hurtSfx, this.transform.position);

        lastDamageTime = Time.time;

        if (player.playerHealth <= 0 && !isDead) //bool isDead is there so that the corutine only runs once per death
        {
            StartCoroutine(death());
        }
    }

    private void healthRecovery()
    {
        player.playerHealth += recoveryRate;

        if (player.playerHealth >= player.maxHealth)
        {
            player.playerHealth = player.maxHealth;
        }
    }

    private IEnumerator death()
    {
        //death
        isDead = true;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.deathSfx, this.transform.position);
        this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        this.gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;
        Instantiate(deathEffect, this.transform.position, this.transform.rotation);


        yield return new WaitForSeconds(deathTimer);
        //respawn
        isDead = false;
        //get random point nearby to respawn
        this.transform.root.position = (Random.insideUnitSphere * respawnRadius) + this.transform.position;
        //rest of respawn logic; allow collisions and render visible
        AudioManager.instance.PlayOneShot(FMODEvents.instance.respawnSfx, this.transform.position);
        this.gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;
        this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;

        aiBrain.AIplayer = playerAI.wander; //set to default enum state

    }
}
