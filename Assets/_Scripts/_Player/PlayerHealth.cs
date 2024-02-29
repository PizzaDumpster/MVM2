using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HealthData
{
    public int currentMaxHealth;
    public int currentHealth;
}

public class PlayerDeath : BaseMessage { }

public class PlayerCurrentHealth : BaseMessage { public HealthData healthData; }

public class PlayerRestoreHealth : BaseMessage { public int restoreAmount; }

public class PlayerHealth : Health , IDamageable
{
    public TriggerStringSO trigger;

    public PlayerCurrentHealth currentHealth = new PlayerCurrentHealth();
    Animator anim;
    private void Start()
    {
        MessageBuffer<PlayerRestoreHealth>.Subscribe(RestoreHealth);
        currentHealth.healthData.currentMaxHealth = maxHealth;
        HealthDispatch();
        anim = GetComponent<Animator>();
    }

    public void OnEnable()
    {
        HealthAmount = maxHealth;
        HealthDispatch();
    }

    public void Damage(int damage)
    {
        HealthAmount = HealthAmount - damage;

        HealthDispatch();
        
        if(HealthAmount <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(PlayerRestoreHealth msg)
    {
        HealthAmount = HealthAmount + msg.restoreAmount;
        HealthDispatch();
    }

    public void Die()
    {
        MessageBuffer<PlayerDeath>.Dispatch();
        OnDeath?.Invoke(); 
    }
    private void HealthDispatch()
    {
        currentHealth.healthData.currentHealth = HealthAmount;
        MessageBuffer<PlayerCurrentHealth>.Dispatch(currentHealth);
    }
}
