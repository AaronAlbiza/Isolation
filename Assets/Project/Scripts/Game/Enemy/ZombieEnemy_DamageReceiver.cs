using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieEnemy_DamageReceiver : MonoBehaviour
{
    private bool dead = false;
    public bool Dead { get { return dead; } }

    private ZombieEnemy_StatsController stats;
    private float currentHealth;
    private GameController gameController;
    private new AudioSource audio;
    private Animator animator;

    void Awake()
    {
        stats = GameObject.Find("RoundController").GetComponent<ZombieEnemy_StatsController>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        animator = GetComponent<Animator>();
        audio = this.GetComponent<AudioSource>();
        currentHealth = stats.currentHealth;
        SetRigidBodyState(true);
        SetColliderState(false);
    }

    public void Gethit(float damage)
    {
        //Take dmg
        currentHealth -= damage;

        animator.SetTrigger("TakeDamage");

        //Play audio 
        audio.PlayOneShot(audio.clip);

        //Check if dead 
        if (currentHealth <= 0)
        {
            Die();
        } 
    }

    public void Die()
    {
        dead = true;
        Destroy(gameObject, 4f);
        GetComponent<Animator>().enabled = false;
        GetComponent<ZombieEnemy_Movement>().enabled = false;
        GetComponent<ZombieEnemy_CombatController>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        SetRigidBodyState(false);
        SetColliderState(true);
        gameController.BroadcastMessage("ZombieDeath", SendMessageOptions.DontRequireReceiver);
    }

    void SetRigidBodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }

    void SetColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach(Collider collider in colliders)
        {
            collider.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }
}
