using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTrigger : DamageTrigger
{
    public int DamageDealt;
     
    public override void TriggerDamage(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            IDamageable damageable = Collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(DamageDealt); // Call the Damage method on the IDamageable component
            }
        }
    }
}
