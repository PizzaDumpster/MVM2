using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HealthData
{
    public int currentMaxHealth;
    public int currentHealth;
}

public class PlayerDeath : BaseMessage { }

public class PlayerCurrentHealth : BaseMessage 
{ 
    public HealthData healthData; 
}
public class PlayerHealth : Health , IDamageable
{
    public int maxHealth;
    PlayerCurrentHealth currentHealth = new PlayerCurrentHealth();
    Animator anim;
    private void Start()
    {
        currentHealth.healthData.currentMaxHealth = 100;
        anim = GetComponent<Animator>();
    }
    public void Damage(int damage)
    {
        HealthAmount = HealthAmount - damage;
        currentHealth.healthData.currentHealth = HealthAmount;
        
        MessageBuffer<PlayerCurrentHealth>.Dispatch(currentHealth);
        
        if(HealthAmount <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        anim.SetTrigger("death");
        yield return null;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        yield return null;
        MessageBuffer<PlayerDeath>.Dispatch();
        OnDeath?.Invoke();
        
    }

}
