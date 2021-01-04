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
    public RoundController roundController;
    public RoundMenu roundMenu;
    public UImanager uiManager;
    public Weapon m4;
    public Weapon m107;
    public Weapon m1911;

    [Header("HUD")]
    public Text ammoText;
    public Text healthText;
    public Text enemyText;
    public Text roundText;
    public Text hudPoints;

    [Header("UPGRADE MENU TEXTS")]
    public Text playerPoints;
    public Text m4_FR_Upgrade_Cost;
    public Text m4_DMG_Upgrade_Cost;
    public Text m4_MAG_Upgrade_Cost;
    public Text m4_RS_Upgrade_Cost;
    public Text m1911_FR_Upgrade_Cost;
    public Text m1911_DMG_Upgrade_Cost;
    public Text m1911_MAG_Upgrade_Cost;
    public Text m1911_RS_Upgrade_Cost;
    public Text m107_FR_Upgrade_Cost;
    public Text m107_DMG_Upgrade_Cost;
    public Text m107_MAG_Upgrade_Cost;
    public Text m107_RS_Upgrade_Cost;
    public Text ammoCost;
    public Text healthCost;


    private int displayedMagAmmo;
    private int displayedTotalAmmo;


    // Start is called before the first frame update
    void Start()
    {
        displayedMagAmmo = player.CurrentWeapon.currentMagAmmo;
        displayedTotalAmmo = player.CurrentWeapon.currentTotalAmmo;
        ammoText.text = "Ammo: " + player.CurrentWeapon.currentMagAmmo + "/" + player.CurrentWeapon.currentTotalAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthController.currentHealth <= 0)
        {
            uiManager.Pause();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        hudPoints.text = "Points: " + player.points;
        playerPoints.text = "Points: " + player.points;

        int aliveEnemies = 0;
        foreach (ZombieEnemy_DamageReceiver enemy in enemyContainer.GetComponentsInChildren<ZombieEnemy_DamageReceiver>())
        {
            if (enemy.Dead == false)
            {
                aliveEnemies++;
            }
        }

        enemyText.text = "Enemies: " + aliveEnemies;
        roundText.text = "Round: " + roundController.CurrentRound;
        healthText.text = "Health: " + healthController.currentHealth;

        if (displayedMagAmmo != player.CurrentWeapon.currentMagAmmo || displayedTotalAmmo != player.CurrentWeapon.currentTotalAmmo)
        {
            ammoText.text = "Ammo: " + player.CurrentWeapon.currentMagAmmo + "/" + player.CurrentWeapon.currentTotalAmmo;
            displayedMagAmmo = player.CurrentWeapon.currentMagAmmo;
            displayedTotalAmmo = player.CurrentWeapon.currentTotalAmmo;
        }

        m4_DMG_Upgrade_Cost.text = "Damage \nCost: " + m4.dmgUpgradeCost;
        m4_FR_Upgrade_Cost.text = "Fire Rate \nCost: " + m4.frUpgradeCost;
        m4_MAG_Upgrade_Cost.text = "Mag Size \nCost: " + m4.magUpgradeCost;
        m4_RS_Upgrade_Cost.text = "Reload Speed \nCost: " + m4.reloadSpeedUpgradeCost;

        m1911_DMG_Upgrade_Cost.text = "Damage \nCost: " + m1911.dmgUpgradeCost;
        m1911_FR_Upgrade_Cost.text = "Fire Rate \nCost: " + m1911.frUpgradeCost;
        m1911_MAG_Upgrade_Cost.text = "Mag Size \nCost: " + m1911.magUpgradeCost;
        m1911_RS_Upgrade_Cost.text = "Reload Speed \nCost: " + m1911.reloadSpeedUpgradeCost;

        m107_DMG_Upgrade_Cost.text = "Damage \nCost: " + m107.dmgUpgradeCost;
        m107_FR_Upgrade_Cost.text = "Fire Rate \nCost: " + m107.frUpgradeCost;
        m107_MAG_Upgrade_Cost.text = "Mag Size \nCost: " + m107.magUpgradeCost;
        m107_RS_Upgrade_Cost.text = "Reload Speed \nCost: " + m107.reloadSpeedUpgradeCost;

        ammoCost.text = "Refill Ammo \nCost: " + player.CurrentWeapon.ammoCost;
        healthCost.text = "Refill Health \nCost: " + healthController.healthCost;
    }
}
