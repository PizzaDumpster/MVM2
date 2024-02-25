using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDamage : DamageTrigger
{
    public int damageInflicted = 100;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(damageInflicted); // Call the Damage method on the IDamageable component
        }
    }
}
