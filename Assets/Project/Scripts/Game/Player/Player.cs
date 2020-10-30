using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("Gameplay")]
    public int currentTotalAmmo = 0;
    public int points = 0;
    public bool canShoot;

    private Weapon currentWeapon;
    public Weapon CurrentWeapon { get { return currentWeapon; } set { currentWeapon = value;} }


    // Start is called before the first frame update
    void Start()
    {
        currentTotalAmmo = currentWeapon.maxAmmo;
        canShoot = true;
    }

    void ZombieDeath()
    {
        points += 100;
    }
}
