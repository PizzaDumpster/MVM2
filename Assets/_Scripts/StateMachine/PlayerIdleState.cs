using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    [Header("States")]
    public PlayerState jumpState;
    public PlayerState attackState;
    public PlayerState moveState;
    public PlayerState dashState;

    [Header("Animations")]
    public TriggerStringSO animationTrigger;
    public float transitionDuration = 0.0f;

    [Header("Power Up")]
    public PowerUpSO dash;

    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        stateMachine.PlayerRigidBody.sharedMaterial = null;

        stateMachine.PlayerAnimator.CrossFade(animationTrigger.triggerString, transitionDuration);
    }

    public override void UpdateState()
    {
        if (stateMachine.PlayerInput.GetPrimaryAxis().x != 0)
        {
            stateMachine.TransitionToState(moveState);
        }

        if (stateMachine.PlayerInput.IsJumpPressed())
        {
            stateMachine.TransitionToState(jumpState);
        }

        if (stateMachine.PlayerInput.IsAttackPressed())
        {
            stateMachine.TransitionToState(attackState);
        }

        if (stateMachine.PlayerInput.IsDashPressed() && stateMachine.CanDash() && stateMachine.unlockedAbilities.Contains(dash))
        {
            stateMachine.TransitionToState(dashState);
        }
    }
}
