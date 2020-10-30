using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4_8 : Weapon
{
    void Start()
    {
        currentMagAmmo = magSize;
        currentMagAmmo = magSize;
        m_animator.SetFloat("ReloadSpeed", reloadSpeed);
        Audio = this.GetComponent<AudioSource>();
    }
}
