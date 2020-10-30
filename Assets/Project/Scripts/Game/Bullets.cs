using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{

    public float speed = 8f;
    public float lifeDuration = 2f;
    public Player player;
    public ParticleSystem hitEffect;
    public Camera playerCamera;

    private float lifeTimer;

    private bool shotByPlayer;
    public bool ShotByPlayer { get { return shotByPlayer; } set { shotByPlayer = value; } }

    // Start is called before the first frame update
    void OnEnable()
    {
        lifeTimer = lifeDuration;
        player = GameObject.Find("Player").GetComponent<Player>();
        playerCamera = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Make the bullet move 
        transform.position += playerCamera.transform.forward * player.CurrentWeapon.bulletSpeed * Time.deltaTime;

        //Check if the bullet should be destroyed
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        ParticleSystem _hitEffect = Instantiate(hitEffect, this.transform.position, this.transform.rotation);
        _hitEffect.Play();
        Destroy(_hitEffect, 2f);
        gameObject.SetActive(false);

        if (other.transform.tag == "Enemy")
        {
            other.GetComponent<ZombieEnemy_DamageReceiver>().Gethit((float)player.CurrentWeapon.damage);
        }
    }

    
}
