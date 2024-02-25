using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health, IDamageable
{
    [SerializeField] GameObject enemyHit; 
    public void Damage(int damage = 0)
    {
        HealthAmount = HealthAmount - damage;
        if (HealthAmount <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}
