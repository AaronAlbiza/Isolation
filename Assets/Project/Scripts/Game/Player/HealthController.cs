using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    public float currentHealth;
    public float totalHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
<<<<<<< Updated upstream
=======

    public void BuyHealth()
    {
        if (player.points >= healthCost)
        {
            currentHealth = totalHealth;
            player.points -= healthCost;
            healthCost = healthCost * 2;
        }
    }
>>>>>>> Stashed changes
}
