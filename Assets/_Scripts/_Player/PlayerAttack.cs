using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponEquiped weaponEquiped;

    void Start()
    {
        weaponEquiped = GetComponentInParent<WeaponEquiped>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.Damage(weaponEquiped.currentWeapon.WeaponDamage);
                    Debug.Log("Damageable object hit: " + other.gameObject.name);
                }
            }
    }
}
