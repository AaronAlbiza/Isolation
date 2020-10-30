using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieEnemy_Movement : MonoBehaviour
{
    private Player player;
    private NavMeshAgent agent;
    private Animator animator;
    private Rigidbody rigidBody;
    
    public int animCooldown = 0;
    public bool canMove = true;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        agent.SetDestination(player.transform.position);

        float speed = this.agent.velocity.magnitude;
        this.animator.SetFloat("WalkSpeed", speed);
    }
}
