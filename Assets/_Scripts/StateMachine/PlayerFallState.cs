using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : PlayerState
{
    public PlayerState idleState;
    public PlayerState dashState;
    public PlayerState jumpState;
    public PlayerState wallSlideState;

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

        if (stateMachine.PlayerInput.IsDashPressed() && stateMachine.CanDash())
        {
            stateMachine.TransitionToState(dashState);
        }

        if (stateMachine.IsWalled())
        {
            stateMachine.TransitionToState(wallSlideState);
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
