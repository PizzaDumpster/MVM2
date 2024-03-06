using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : PlayerState
{
    [Header("States")]
    public PlayerState idleState;
    public PlayerState dashState;
    public PlayerState jumpState;
    public PlayerState wallSlideState;

    [Header("Animations")]
    public TriggerStringSO animationTrigger;
    public float transitionDuration = 0.0f;

    [Header("Power Up")]
    public PowerUpSO wallslide;
    public PowerUpSO dash;

    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);
        print(stateMachine.PlayerRigidBody.velocity);
        if(stateMachine.PlayerRigidBody.velocity != Vector2.zero) stateMachine.PlayerAnimator.CrossFade(animationTrigger.triggerString, transitionDuration);
    }
    public override void UpdateState()
    {

        CheckForChange();
    }

    private void CheckForChange()
    {
        if (stateMachine.GroundCheck.IsGrounded() || stateMachine.PlayerRigidBody.velocity == Vector2.zero)
        {
            stateMachine.TransitionToState(idleState);
        }

        if (stateMachine.PlayerInput.IsDashPressed() && stateMachine.CanDash() && stateMachine.unlockedAbilities.Contains(dash))
        {
            stateMachine.TransitionToState(dashState);
        }

        if (stateMachine.WallCheck.IsWalled() && stateMachine.unlockedAbilities.Contains(wallslide))
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
