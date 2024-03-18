using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthBar : BaseMessage { public HealthData healthData; }

public class BossFightStarted : BaseMessage { public string BossName; }

public class BossFightEnded : BaseMessage { }

public class BossHealth : Health, IDamageable
{
    public UnityEvent onHit;
    public EnemyHealthBar bossHealth = new EnemyHealthBar();
    public string bossName;
    private BossFightStarted bossInfo = new BossFightStarted();

    public void Awake()
    {
        MessageBuffer<PlayerRespawn>.Subscribe(RespawnCharacterIn);
        bossHealth.healthData.currentMaxHealth = maxHealth;
        bossInfo.BossName = bossName;
    }

    private void RespawnCharacterIn(PlayerRespawn obj)
    {
        HealthAmount = maxHealth;
        HealthDispatch();
    }

    public void OnEnable()
    {
        bossHealth.healthData.currentHealth = HealthAmount;
        HealthDispatch();
        MessageBuffer<BossFightStarted>.Dispatch(bossInfo);
    }
    public void Damage(int damage = 0, Transform transform = null)
    {

        HealthAmount = HealthAmount - damage;

        onHit?.Invoke();

        HealthDispatch();

        if (HealthAmount <= 0)
        {
            OnDeath?.Invoke();
            MessageBuffer<BossFightEnded>.Dispatch();
        }
    }

    private void HealthDispatch()
    {
        bossHealth.healthData.currentHealth = HealthAmount;
        MessageBuffer<EnemyHealthBar>.Dispatch(bossHealth);
    }

    
}
