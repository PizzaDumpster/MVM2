using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerState idleState;
    public PlayerState fallState;

    [Header("")]
    public TriggerStringSO animationTrigger;
    private bool animationFinished = false;


    [Header("")]
    public Transform attackPoint;
    public float direction;

    [Header("")]
    public WeaponEquiped weapon;
    

    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        stateMachine.PlayerAnimator.CrossFade(animationTrigger.triggerString, 0.0f);
        animationFinished = false;
    }

    private float lastAttackTime; // Time of the last attack

    public float attackCooldown = 0.5f; // Cooldown duration in seconds

    public override void UpdateState()
    {
        // Check if enough time has passed since the last attack
        if (Time.time - lastAttackTime < attackCooldown)
            return; // Exit the method if still in cooldown

        // Perform attack logic
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, 1f);
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                continue;
            }

            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(weapon.currentWeapon.WeaponDamage, attackPoint);
            }
        }

        // Update last attack time
        lastAttackTime = Time.time;


        if (!animationFinished && stateMachine.PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            if (stateMachine.GroundCheck.IsGrounded())
            {
                stateMachine.TransitionToState(idleState);
                animationFinished = true;
            }
            else
            {
                stateMachine.TransitionToState(fallState);
                animationFinished = true;
            }

        }
    }
}
