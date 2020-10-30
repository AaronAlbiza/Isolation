using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{

    public float hitDuration = 0.5f;
    public AudioSource m_AudioSource;
    private Player player;
    private HealthController healthController;

    private bool isHit = false;

    // Start is called before the first frame update
    void Awake()
    {
        //m_AudioSource = GetComponent<AudioSource>();
        player = GetComponent<Player>();
        healthController = GetComponent<HealthController>();
    }

    //Check for collisions
    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.GetComponent<AmmoCrate>() != null)
        {
            Debug.Log("Ammo crate collision");
            //Ammo crate collision
            AmmoCrate ammoCrate = otherCollider.GetComponent<AmmoCrate>();
            player.currentTotalAmmo += ammoCrate.ammo;

            Destroy(ammoCrate.gameObject);
        }
    }

    public void GetHit(float damage)
    {
        if (isHit == false)
        {
            StartCoroutine(HitRoutine(damage));
        }
    }

    IEnumerator HitRoutine(float damage)
    {
        isHit = true;
        healthController.TakeDamage(damage);
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        //Shift camera to feel hit impact
        

        yield return new WaitForSeconds(hitDuration);

        isHit = false;
    }
}
