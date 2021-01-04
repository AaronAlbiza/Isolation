using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("Gameplay")]
    public int points = 0;
    public bool canShoot;
    public List<Weapon> inventory;
    public List<Weapon> weapons;

    private int selectedWeapon = 0;
    private Weapon currentWeapon;
    public Weapon CurrentWeapon { get { return currentWeapon; } set { currentWeapon = value;} }


    // Start is called before the first frame update
    void Awake()
    {
        EquipM1911();
        canShoot = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown("1") && selectedWeapon != 0)
        {
            if (inventory[0] != null)
            {
                EquipM1911();
            }
        }

        if (Input.GetKeyDown("2") && selectedWeapon != 1)
        {
            if (inventory[1] != null)
            {
                EquipM4();
            }
        }

        if (Input.GetKeyDown("3") && selectedWeapon != 2)
        {
            if (inventory[2] != null)
            {
                EquipM107();
            }
        }

    }

    void ZombieDeath()
    {
        points += 100;
    }

    public void EquipM4()
    {
        selectedWeapon = 1;
        equipWeapon(selectedWeapon);
    }

    public void EquipM107()
    {
        selectedWeapon = 2;
        equipWeapon(selectedWeapon);
    }

    public void EquipM1911()
    {
        selectedWeapon = 0;
        equipWeapon(selectedWeapon);     
    }

    public void equipWeapon(int whichWeapon)
    {
        int i = 0;
        foreach (Weapon weapon in weapons)
        {
            if (i == whichWeapon)
            {
                weapon.gameObject.SetActive(true);
                currentWeapon = weapon;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public void BuyAmmo()
    {
        if (points >= currentWeapon.ammoCost)
        {
            currentWeapon.currentTotalAmmo = currentWeapon.maxAmmo;
            points -= currentWeapon.ammoCost;
        }
    }
}
