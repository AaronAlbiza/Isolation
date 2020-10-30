using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [Header("Game")]
    public Player player;
    public HealthController healthController;
    public GameObject enemyContainer;
    public EnemySpawner enemySpawner;
    public RoundMenu roundMenu;
    public Weapon m4;
    public Weapon m107;
    public Weapon m1911;

    [Header("HUD")]
    public Text ammoText;
    public Text healthText;
    public Text enemyText;
    public Text roundText;
    public Text hudPoints;

    [Header("WeaponMenu")]
    public Text m4Option;
    public Text m1911Option;
    public Text m107Option;
    public Text weapon4;
    public Text weaponMenuPoints;

    [Header("UpgradeMenu")]
    public Text damageUpgradeText;
    public Text rofUpgradeText;
    public Text magUpgradeText;
    public Text healthUpgradeText;
    public Text upgradeMenuPoints;


    private int displayedMagAmmo;
    private int displayedTotalAmmo;


    // Start is called before the first frame update
    void Start()
    {
        displayedMagAmmo = player.CurrentWeapon.currentMagAmmo;
        displayedTotalAmmo = player.currentTotalAmmo;
        ammoText.text = "Ammo: " + player.CurrentWeapon.currentMagAmmo + "/" + player.currentTotalAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthController.currentHealth <= 0)
        {
            roundMenu.Pause();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        upgradeMenuPoints.text = "Points: " + player.points;
        weaponMenuPoints.text = "Points: " + player.points;
        hudPoints.text = "Points: " + player.points;

        int aliveEnemies = 0;
        foreach (ZombieEnemy_DamageReceiver enemy in enemyContainer.GetComponentsInChildren<ZombieEnemy_DamageReceiver>())
        {
            if (enemy.Dead == false)
            {
                aliveEnemies++;
            }
        }

        enemyText.text = "Enemies: " + aliveEnemies;
        roundText.text = "Round: " + enemySpawner.Round;
        healthText.text = "Health: " + healthController.currentHealth;

        if (displayedMagAmmo != player.CurrentWeapon.currentMagAmmo || displayedTotalAmmo != player.currentTotalAmmo)
        {
            ammoText.text = "Ammo: " + player.CurrentWeapon.currentMagAmmo + "/" + player.currentTotalAmmo;
            displayedMagAmmo = player.CurrentWeapon.currentMagAmmo;
            displayedTotalAmmo = player.currentTotalAmmo;
        }

        m4Option.text = "M4A1\n" + "Cost: " + m4.cost;
        m107Option.text = "M107\n" + "Cost: " + m107.cost;
        m1911Option.text = "M1911\n" + "Cost: " + m1911.cost;

        damageUpgradeText.text = "Damage Upgrade: " + roundMenu.baseDamageUpgrade + "\n" + "Cost: " + roundMenu.damageUpgradeCost + "\nDamage: " + player.CurrentWeapon.damage;
        rofUpgradeText.text = "Fire Rate Upgrade: " + roundMenu.baseFireRateUpgrade + "\n" + "Cost: " + roundMenu.fireRateUpgradeCost + "\nFire Rate: " + player.CurrentWeapon.fireRate;
        magUpgradeText.text = "Mag Upgrade :" + roundMenu.baseMagUpgrade + "\n" + "Cost: " + roundMenu.magUpgradeCost + "\nMag Size: " + player.CurrentWeapon.magSize;
        healthUpgradeText.text = "Health Upgrade :" + roundMenu.baseHealthUpgrade + "\n" + "Cost: " + roundMenu.healthUpgradeCost + "\nHealth: " + healthController.currentHealth;

        if (Input.GetKeyDown("u"))
        {
            roundMenu.Pause();
        }
    }
}
