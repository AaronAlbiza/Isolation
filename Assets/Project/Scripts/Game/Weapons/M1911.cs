using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1911 : Weapon
{
    void Start()
    {
        currentMagAmmo = magSize;
        m_animator.SetFloat("ReloadSpeed", reloadSpeed);
        Audio = this.GetComponent<AudioSource>();
    }
}
