using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieEnemy_CombatController : MonoBehaviour
{
    public Collider hurtBox;
    public Animator animator;
    public float attackRange = 2;
    public bool canAttack;


    private ZombieEnemy_StatsController stats;
    private AnimatorClipInfo[] currentClipInfo;
    private float damage;
    private GameObject player;
    private ZombieEnemy_Movement movementController;
    private NavMeshAgent agent;

    void Awake()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        movementController = GetComponent<ZombieEnemy_Movement>();
        stats = GameObject.Find("RoundController").GetComponent<ZombieEnemy_StatsController>();
        damage = stats.currentDamage;
    }
    
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (attackRange >= distance && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        //agent.isStopped = true;
        
        //movementController.canMove = false;
        animator.SetTrigger("Attack");

        currentClipInfo = this.animator.GetCurrentAnimatorClipInfo(0);

        yield return new WaitForSeconds(currentClipInfo[0].clip.length);

        movementController.canMove = true;
        agent.isStopped = false;
        canAttack = true;
    }

    public void DealDamage()
    {
        Collider[] cols = Physics.OverlapBox(hurtBox.bounds.center, hurtBox.bounds.extents, hurtBox.transform.rotation, LayerMask.GetMask("HitBox"));

        foreach (Collider c in cols)
        {
            if (c.transform.tag == "Player")
            {
                Debug.Log("Hit player");
                c.transform.SendMessage("GetHit", damage);
            }
        }
    }
}
