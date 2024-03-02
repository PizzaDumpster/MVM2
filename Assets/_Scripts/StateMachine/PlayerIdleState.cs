using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerState jumpState;
    public PlayerState attackState;
    public PlayerState moveState;

    public TriggerStringSO animationTrigger;
    public float transitionDuration = 0.0f;

    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        stateMachine.PlayerAnimator.CrossFade(animationTrigger.triggerString, transitionDuration);
    }

    public override void UpdateState()
    {
        if (stateMachine.m_InputAxis.x != 0)
        {
            stateMachine.TransitionToState(moveState);
        }

        if (stateMachine.PlayerInput.isJumpPressed())
        {
            stateMachine.TransitionToState(jumpState);
        }

        if (stateMachine.PlayerInput.IsAttackPressed())
        {
            stateMachine.TransitionToState(attackState);
        }
    }
}
