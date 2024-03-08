using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [Header("")]
    public UnityEvent onHit;

    public PlayerCurrentHealth currentHealth = new PlayerCurrentHealth();
    private Animator anim;
    private void Start()
    {
        MessageBuffer<PlayerRespawn>.Subscribe(RespawnCharacterIn);
        MessageBuffer<PlayerRestoreHealth>.Subscribe(RestoreHealth);
        currentHealth.healthData.currentMaxHealth = maxHealth;
        HealthDispatch();
        anim = GetComponent<Animator>();
    }

    private void RespawnCharacterIn(PlayerRespawn obj)
    {
        HealthAmount = maxHealth;
        HealthDispatch();
    }

    public void Damage(int damage, Transform transform)
    {
        HealthAmount = HealthAmount - damage;

        HealthDispatch();
        
        if(HealthAmount <= 0)
        {
            Die();
            return;
        }

        onHit?.Invoke();
        print("Hit");
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
