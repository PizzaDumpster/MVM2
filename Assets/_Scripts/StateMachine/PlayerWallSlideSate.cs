using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideSate : PlayerState
{
    public PlayerState jumpState;
    public PlayerState idleState;

    public TriggerStringSO animationTrigger;
    public float transitionDuration = 0.0f;

    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);
        stateMachine.PlayerAnimator.CrossFade(animationTrigger.triggerString, transitionDuration);
    }

    public override void UpdateState()
    {
        CheckForChange();
    }

    private void CheckForChange()
    {
        if (stateMachine.GroundCheck.IsGrounded())
        {
            stateMachine.TransitionToState(idleState);
        }
        if (stateMachine.PlayerInput.IsJumpPressed())
        {
            stateMachine.TransitionToState(jumpState);
        }
    }

    public override bool CanWallJump()
    {
        return true;
    }
}
