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

    [Header("Required")]
    public GameObject playerHud;
    public GameObject pauseMenuUI;
    public GameObject upgradeMenuUI;
    public GameObject prePurchaseM4Menu;
    public GameObject prePurchaseM107Menu;
    public GameObject postPurchaseM4Menu;
    public GameObject postPurchaseM107Menu;
    public GameObject postPurchaseM1911Menu;
    public GameObject m4Menus;
    public GameObject m107Menus;
    public GameObject m1911Menus;
    public HealthController healthController;
    public Player player;    
    public bool gamePaused = false;

    void Awake()
    {
       
        
    }

    /*public void Resume()
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
    }*/

    public void UpgradeMenu()
    {
        pauseMenuUI.SetActive(false);
        upgradeMenuUI.SetActive(true);
    }

    public void PurchaseM4()
    {
        if (player.points >= m4.cost)
        {
            player.inventory.Insert(1, m4);
            player.inventory.RemoveAt(2);
            OpenPostPurchaseM4Menu();
            player.points -= 1000;
        }
    }

    public void PurchaseM107()
    {
        if (player.points >= m107.cost)
        {
            player.inventory.Insert(2, m107);
            player.inventory.RemoveAt(3);
            OpenPostPurchaseM107Menu();
            player.points -= 1500;
        }
    }


    public void M4MenuButton()
    {
        m4Menus.SetActive(true);
        m107Menus.SetActive(false);
        m1911Menus.SetActive(false);

        if (player.inventory[1] == null)
        {
            OpenPrePurchaseM4Menu();
        }
        else
        {
            OpenPostPurchaseM4Menu();
        }
    }

    public void M107MenuButton()
    {
        m4Menus.SetActive(false);
        m107Menus.SetActive(true);
        m1911Menus.SetActive(false);

        if (player.inventory[2] == null)
        {
            OpenPrePurchaseM107Menu();
        }
        else
        {
            OpenPostPurchaseM107Menu();
        }
    }

    public void M1911MenuButton()
    {
        m4Menus.SetActive(false);
        m107Menus.SetActive(false);
        m1911Menus.SetActive(true);
        OpenPostPurchaseM1911Menu();
    }

    //Pre Purchase Menus
    public void OpenPrePurchaseM4Menu()
    {
        prePurchaseM4Menu.SetActive(true);
        prePurchaseM107Menu.SetActive(false);
        postPurchaseM4Menu.SetActive(false);
        postPurchaseM107Menu.SetActive(false);
        postPurchaseM1911Menu.SetActive(false);
}

    public void OpenPrePurchaseM107Menu()
    {
        prePurchaseM4Menu.SetActive(false);
        prePurchaseM107Menu.SetActive(true);
        postPurchaseM4Menu.SetActive(false);
        postPurchaseM107Menu.SetActive(false);
        postPurchaseM1911Menu.SetActive(false);
    }

    //Post Purchase Menus
    public void OpenPostPurchaseM4Menu()
    {
        prePurchaseM4Menu.SetActive(false);
        prePurchaseM107Menu.SetActive(false);
        postPurchaseM4Menu.SetActive(true);
        postPurchaseM107Menu.SetActive(false);
        postPurchaseM1911Menu.SetActive(false);
    }

    public void OpenPostPurchaseM107Menu()
    {
        postPurchaseM107Menu.SetActive(true);
        prePurchaseM4Menu.SetActive(false);
        prePurchaseM107Menu.SetActive(false);
        postPurchaseM4Menu.SetActive(false);
        postPurchaseM1911Menu.SetActive(false);
    }

    public void OpenPostPurchaseM1911Menu()
    {
        prePurchaseM4Menu.SetActive(false);
        prePurchaseM107Menu.SetActive(false);
        postPurchaseM4Menu.SetActive(false);
        postPurchaseM107Menu.SetActive(false);
        postPurchaseM1911Menu.SetActive(true);
    }
}
