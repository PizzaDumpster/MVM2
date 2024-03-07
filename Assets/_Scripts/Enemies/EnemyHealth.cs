using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : Health, IDamageable
{
    public UnityEvent onHit;
    public void Damage(int damage = 0, Transform transform = null)
    {
        print("Damage");
        HealthAmount = HealthAmount - damage;

        onHit?.Invoke();

        if (HealthAmount <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}
