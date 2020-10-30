using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [Header("Stats")]
    public int damage;
    public float range = 100f;
    public int bulletSpeed;
    public float reloadSpeed;
    public bool automatic;
    public int magSize;
    public int maxAmmo;
    public float fireRate;
    public int cost;

    [Header("Required")]
    public GameObject gunBarrel;
    public Player player;
    public Animator m_animator;
    public ParticleSystem muzzleFlash;
    public Camera fpsCam;
    public RoundMenu roundMenu;

    public int currentMagAmmo;

    private float lastFired;
    private new AudioSource audio;
    private bool reloading = false;

    public AudioSource Audio { set { audio = value; } }

    void Start()
    {
        m_animator.SetFloat("ReloadSpeed", reloadSpeed);
    }

    void Update()
    {
        if (!roundMenu.gamePaused)
        {
            if (reloading)
            {
                if (this.m_animator.GetCurrentAnimatorStateInfo(0).IsName("Reloading"))
                {
                    player.canShoot = false;
                }
                if (!this.m_animator.GetCurrentAnimatorStateInfo(0).IsName("Reloading"))
                {
                    player.canShoot = true;
                    reloading = false;
                    Reload();
                }
            }

            if (Input.GetMouseButtonDown(0) && player.canShoot && !automatic)
            {
                if (Time.time - lastFired > 1 / fireRate && currentMagAmmo > 0)
                {
                    lastFired = Time.time;
                    currentMagAmmo--;
                    m_animator.SetTrigger("Shooting");
                    audio.PlayOneShot(audio.clip);
                    muzzleFlash.Play();
                    Shoot();
                }
            }

            if (Input.GetMouseButton(0) && player.canShoot && automatic)
            {
                if (Time.time - lastFired > 1 / fireRate && currentMagAmmo > 0)
                {
                    lastFired = Time.time;
                    currentMagAmmo--;
                    m_animator.SetTrigger("Shooting");
                    audio.PlayOneShot(audio.clip);
                    muzzleFlash.Play();
                    Shoot();
                }
            }

            if (Input.GetKeyDown("r") && !reloading)
            {
                if (player.currentTotalAmmo > 0 && currentMagAmmo < magSize)
                {
                    reloading = true;
                    m_animator.SetTrigger("Reload");
                }
            }
        }
    }

    public void Reload()
    {
        int missingAmmo = magSize - currentMagAmmo;
        if (player.currentTotalAmmo < missingAmmo)
        {
            currentMagAmmo += player.currentTotalAmmo;
            player.currentTotalAmmo = 0;
        }

        else
        {
            currentMagAmmo += missingAmmo;
            player.currentTotalAmmo -= missingAmmo;
        }
    } 

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            
            Debug.Log(hit.transform.name);

            if (hit.transform.tag == "Enemy")
            {
                Debug.Log(damage);
                hit.transform.GetComponent<ZombieEnemy_DamageReceiver>().Gethit(damage);
            }
        }
    }
}
