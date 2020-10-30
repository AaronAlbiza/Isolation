using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundMenu : MonoBehaviour
{
    [Header("Weapons")]
    public Weapon m1911;
    public Weapon m4;
    public Weapon m107;
    public int selectedWeapon = 0;

    [Header("Required")]
    public GameObject playerHud;
    public GameObject pauseMenuUI;
    public GameObject upgradeMenuUI;
    public HealthController healthController;

    [Header("Buttons")]
    public Button m1911Button;
    public Button m4Button;
    public Button m107Button;

    [Header("Upgrades")]
    public int baseHealthUpgrade;
    public int healthUpgradeCost;
    public int baseMagUpgrade;
    public int magUpgradeCost;
    public int baseFireRateUpgrade;
    public int fireRateUpgradeCost;
    public int baseDamageUpgrade;
    public int damageUpgradeCost;

    private List<Weapon> weapons;
    private List<Button> buttons;
    

    public bool gamePaused = false;
    
    public Player player;

    void Awake()
    {
        weapons = new List<Weapon>();
        weapons.Add(m1911);
        weapons.Add(m4);
        weapons.Add(m107);

        buttons = new List<Button>();
        buttons.Add(m1911Button);
        buttons.Add(m4Button);
        buttons.Add(m107Button);

        player.CurrentWeapon = weapons[selectedWeapon];

        equipWeapon(selectedWeapon);
    }

    public void Resume()
    {
        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        pauseMenuUI.SetActive(false);
        upgradeMenuUI.SetActive(false);
        playerHud.SetActive(true);
        player.canShoot = true;
        Time.timeScale = 1f;
        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        

        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        playerHud.SetActive(false);
        player.canShoot = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UpgradeMenu()
    {
        pauseMenuUI.SetActive(false);
        upgradeMenuUI.SetActive(true);
    }

    public void WeaponMenu()
    {
        pauseMenuUI.SetActive(true);
        upgradeMenuUI.SetActive(false);
    }

    public void EquipM4()
    {
        if (player.points >= m4.cost)
        {
            selectedWeapon = 1;
            equipWeapon(selectedWeapon);
            player.points -= m4.cost;
        }
    }

    public void EquipM107()
    {
        if (player.points >= m107.cost)
        {
            selectedWeapon = 2;
            equipWeapon(selectedWeapon);
            player.points -= m107.cost;
        }
    }

    public void EquipM1911()
    {
        if (player.points >= m1911.cost)
        {

            selectedWeapon = 0;
            equipWeapon(selectedWeapon);
            player.points -= m1911.cost;
        }
    }

    public void equipWeapon (int whichWeapon)
    {
        int i = 0;
        foreach (Weapon weapon in weapons)
        {
            if (i == whichWeapon)
            {
                weapon.gameObject.SetActive(true);
                player.CurrentWeapon = weapon;
                player.currentTotalAmmo = weapon.maxAmmo;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }

        int j = 0;
        foreach (Button button in buttons)
        {
            if (j == whichWeapon)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
            j++;
        }
    }

    //Place the following in a weapon upgrades manager of some kind
    public void HealthUpgrade()
    {
        if(player.points >= healthUpgradeCost)
        {
            healthController.totalHealth += baseHealthUpgrade;
            healthController.currentHealth += baseHealthUpgrade;
            player.points -= healthUpgradeCost;
        }
        else
        {
            Debug.Log("Yikes nice try");
        }
    }

    public void DamageUpgrade()
    {
        if (player.points >= damageUpgradeCost)
        {
            player.CurrentWeapon.damage += baseDamageUpgrade;
            player.points -= damageUpgradeCost;
        }
        else
        {
            Debug.Log("Yikes nice try");
        }
    }

    public void FireRateUpgrade()
    {
        if (player.points >= fireRateUpgradeCost)
        {
            player.CurrentWeapon.fireRate += baseFireRateUpgrade;
            player.points -= fireRateUpgradeCost;
        }
        else
        {
            Debug.Log("Yikes nice try");
        }
    }

    public void MagUpgrade()
    {
        if (player.points >= magUpgradeCost)
        {
            player.CurrentWeapon.magSize += baseMagUpgrade;
            player.points -= magUpgradeCost;
        }
        else
        {
            Debug.Log("Yikes nice try");
        }
    }
}
