using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{

    public Player player;
    public bool gamePaused = false;

    [Header("Required")]
    public GameObject crossHair;
    public GameObject playerPoints;
    public GameObject pauseMenuUI;
    public GameObject upgradeMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("p"))
        {
            if (!gamePaused)
                Pause();
            else if (gamePaused)
                Resume();
        }

        if (Input.GetKeyDown("u"))
        {
            OpenUpgradeUI();
        }
    }

    public void Resume()
    {
        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        pauseMenuUI.SetActive(false);
        upgradeMenuUI.SetActive(false);
        crossHair.SetActive(true);
        playerPoints.SetActive(true);
        player.canShoot = true;
        Time.timeScale = 1f;
        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        crossHair.SetActive(false);
        playerPoints.SetActive(false);
        player.canShoot = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void OpenUpgradeUI()
    {
        if (gamePaused)
        {
            pauseMenuUI.SetActive(false);
            upgradeMenuUI.SetActive(true);
            //crossHair.SetActive(false);
            //playerPoints.SetActive(false);
        }
        else
        {
            Pause();
            pauseMenuUI.SetActive(false);
            upgradeMenuUI.SetActive(true);
            //crossHair.SetActive(true);
            //playerPoints.SetActive(true);
        }
    }
}
