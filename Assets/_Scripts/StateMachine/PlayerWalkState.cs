using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    [Header("States")]
    public PlayerState idleState;
    public PlayerState jumpState;
    public PlayerState attackState;
    public PlayerState dashState;
    public PlayerState fallState;
    public PlayerState wallSlideState;


    [Header("Animations")]
    public TriggerStringSO animationTrigger;
    public float transitionDuration = 0.3f;

    [Header("Power Up")]
    public PowerUpSO dash;
    public override void EnterState(PlayerStateMachine stateMachine)
    {

        base.EnterState(stateMachine);

        stateMachine.PlayerAnimator.CrossFade(animationTrigger.triggerString, transitionDuration);
    }

    public override void UpdateState()
    {
        if (!stateMachine.GroundCheck.IsGrounded() && stateMachine.WallCheck.IsWalled())
        {
            stateMachine.TransitionToState(wallSlideState);
        }

        if (!stateMachine.GroundCheck.IsGrounded())
        {
            stateMachine.TransitionToState(fallState);
        }

        if (stateMachine.PlayerInput.IsDashPressed() && stateMachine.CanDash() && stateMachine.unlockedAbilities.Contains(dash))
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