using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : BaseMessage { }
public class PlayerHealth : Health , IDamageable
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Damage(int damage)
    {
        HealthAmount = HealthAmount - damage;

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
