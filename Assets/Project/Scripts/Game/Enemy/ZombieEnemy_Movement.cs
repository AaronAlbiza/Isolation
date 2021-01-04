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

    public int stoppingDistance;
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
    void FixedUpdate()
    {
        var distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance > stoppingDistance)
        {
            agent.isStopped = false;
            rigidBody.constraints = RigidbodyConstraints.None;
            Move();
        }
        else
        {
            agent.isStopped = true;
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void Move()
    {
        agent.SetDestination(player.transform.position);

        float speed = this.agent.velocity.magnitude;
        this.animator.SetFloat("WalkSpeed", speed);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rigidBody.constraints = RigidbodyConstraints.None;
        }
    }
}
