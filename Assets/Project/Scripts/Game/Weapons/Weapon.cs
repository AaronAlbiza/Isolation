using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
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
    public int dmgUpgradeAmount;
    public float frUpgradeAmount;
    public int magUpgradeAmount;
    public float reloadSpeedUpgradeAmount;
    public int dmgUpgradeCost;
    public int frUpgradeCost;
    public int magUpgradeCost;
    public int reloadSpeedUpgradeCost;
    public int ammoCost;
    public int currentTotalAmmo;
    public int scalingCost;

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

    void Update()
    {
        m_animator.SetFloat("ReloadSpeed", reloadSpeed);

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
                if (currentTotalAmmo > 0 && currentMagAmmo < magSize)
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
        if (currentTotalAmmo < missingAmmo)
        {
            currentMagAmmo += currentTotalAmmo;
            currentTotalAmmo = 0;
        }

        else
        {
            currentMagAmmo += missingAmmo;
            currentTotalAmmo -= missingAmmo;
        }
    } 

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<ZombieEnemy_DamageReceiver>().Gethit(damage);
            }
        }
    }

    public void DamageUpgrade()
    {
        if (player.points >= dmgUpgradeCost)
        {
            this.damage += dmgUpgradeAmount;
            player.points -= dmgUpgradeCost;
            dmgUpgradeCost += scalingCost;
        }
    }

    public void FireRateUpgrade()
    {
        if (player.points >= frUpgradeCost)
        {
            this.fireRate += frUpgradeAmount;
            player.points -= frUpgradeCost;
            frUpgradeCost += scalingCost;
        }
    }

    public void MagSizeUpgrade()
    {
        if (player.points >= magUpgradeCost)
        {
            this.magSize += magUpgradeAmount;
            player.points -= magUpgradeCost;
            magUpgradeCost += scalingCost;
        }
    }

    public void ReloadSpeedUpgrade()
    {
        if (player.points >= reloadSpeedUpgradeCost)
        {
            this.reloadSpeed += reloadSpeedUpgradeAmount;
            player.points -= reloadSpeedUpgradeCost;
            reloadSpeedUpgradeCost += scalingCost;
        }
    }
}
