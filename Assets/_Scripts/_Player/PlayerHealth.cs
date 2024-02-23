using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health , IDamageable
{
    public void Damage(int damage)
    {
        HealthAmount = HealthAmount - damage;

        if(HealthAmount <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDeath?.Invoke();
    }

}
