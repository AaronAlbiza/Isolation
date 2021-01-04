using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    public float currentHealth;
    public float totalHealth;
    public int healthCost;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
        player = GetComponent<Player>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void BuyHealth()
    {
        if (player.points >= healthCost)
        {
            currentHealth = totalHealth;
            healthCost = healthCost * 2;
            player.points -= healthCost;
        }
    }
}
