using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [Header("Stats")]
    public int baseHealth = 5;
    public int currentHealth = 0;
    public int baseDamage = 5;
    public int currentDamage = 0;
    public int pointValue = 100;
    public float attackRange = 1;
    public float speed = 2;
    public int despawnTimer = 3;
    public int ammoSpawnChance = 15;


    [Header("Requirements")]
    public EnemySpawner spawner;
    public Player player;
    public Animator animator;
    public AmmoCrate ammoCrate;

    private NavMeshAgent agent;
    private BoxCollider hurtBox;
    private new AudioSource audio;
    private int r;

    private bool dead = false;
    public bool Dead { get { return dead; } }

    void Awake()
    {

        spawner = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();
        player = GameObject.Find("Player").GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
        hurtBox = GetComponent<BoxCollider>();
        agent.speed = speed;
        audio = this.GetComponent<AudioSource>();
        r = Random.Range(1, 101);

        for (int i = 0; i < spawner.Round; i++)
        {
            currentHealth += baseHealth;
            currentDamage += baseDamage;
        }

        agent.SetDestination(player.transform.position);


    }

    void Update()
    {
        agent.SetDestination(player.transform.position);

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (attackRange >= distance)
        {
            animator.SetBool("Moving", false);
            animator.SetTrigger("Attack1Trigger");
            hurtBox.enabled = true;
        }
        else
        {
            animator.ResetTrigger("Attack1Trigger");
            animator.SetBool("Moving", true);
            hurtBox.enabled = false;
        }
    }



    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.GetComponent<Bullets>() != null)
        {
            Bullets bullet = otherCollider.GetComponent<Bullets>();
            if (bullet.ShotByPlayer == true)
            {
                audio.PlayOneShot(audio.clip);
                currentHealth -= player.CurrentWeapon.damage;
                bullet.gameObject.SetActive (false);

                if (currentHealth <= 0)
                {
                    if (dead == false)
                    {
                        dead = true;
                        StartCoroutine(OnKill());
                    }
                }
            }
        }
    }

    IEnumerator OnKill()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        animator.SetTrigger("Dead");
        agent.speed = 0;
        player.points += pointValue;

        yield return new WaitForSeconds(despawnTimer);

        if (r <= ammoSpawnChance)
        {
            Instantiate(ammoCrate , this.transform.position, this.transform.rotation);
        }

        Destroy(gameObject);
    }

    public void FootR()
    {
    }

    public void FootL()
    {
    }

    public void Hit()
    {
    }

}
