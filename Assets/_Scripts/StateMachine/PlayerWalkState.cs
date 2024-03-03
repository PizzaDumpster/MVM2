using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerState idleState;
    public PlayerState jumpState;
    public PlayerState attackState;
    public PlayerState dashState;

    public TriggerStringSO animationTrigger;
    public float transitionDuration = 0.3f;

    public override void EnterState(PlayerStateMachine stateMachine)
    {

        base.EnterState(stateMachine);

        stateMachine.PlayerAnimator.CrossFade(animationTrigger.triggerString, transitionDuration);
    }

    public override void UpdateState()
    {
        if (stateMachine.PlayerInput.IsDashPressed() && stateMachine.CanDash())
        {
            stateMachine.TransitionToState(dashState);
        }

        if (stateMachine.PlayerInput.IsJumpPressed())
        {
            stateMachine.TransitionToState(jumpState);
        }

        if (stateMachine.PlayerInput.IsAttackPressed())
        {
            stateMachine.TransitionToState(attackState);
        }

        if(stateMachine.PlayerInput.GetPrimaryAxis().x == 0) 
        {
            stateMachine.TransitionToState(idleState);
        }
    }
}