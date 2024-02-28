using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            
                if (other.gameObject.CompareTag("Player"))
                {
                    return;
                }

                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.Damage(playerController.weapon.currentWeapon.WeaponDamage);
                    Debug.Log("Damageable object hit: " + other.gameObject.name);
                }
            }
    }
}
