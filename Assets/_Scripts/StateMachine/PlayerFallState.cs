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

    public float wallJumpCooldown = 0.25f; // Adjust the cooldown duration as needed
    public float wallJumpTimer = 0.0f;
    public bool isWallJumpCooldownActive = false;
    public bool isWallCountDownDone = false;

    public override void EnterState(PlayerStateMachine stateMachine)
    {
        wallJumpTimer = 0.0f;
        isWallJumpCooldownActive = true;
        isWallCountDownDone = false;
        base.EnterState(stateMachine);
        stateMachine.PlayerAnimator.CrossFade(animationTrigger.triggerString, transitionDuration);
    }
    public override void UpdateState()
    {
        CheckForChange();
        // Update the wall jump timer if it's active
        if (!isWallJumpCooldownActive)
        {
            wallJumpTimer -= Time.deltaTime;
            if (wallJumpTimer <= 0.0f)
            {
                isWallCountDownDone = true;
                
            }
        }
    }

    private void CheckForChange()
    {
        if (stateMachine.GroundCheck.IsGrounded() || stateMachine.PlayerRigidBody.velocity == Vector2.zero)
        {
            stateMachine.TransitionToState(idleState);
        }

        if (stateMachine.PlayerInput.IsDashPressed() && stateMachine.CanDash() && stateMachine.unlockedAbilities.Contains(dash) )
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
        if (stateMachine.previousState == wallSlideState && isWallJumpCooldownActive)
        {
            StartWallJumpCooldown();
            return true;
        }
        else if (!isWallCountDownDone)
        {
            return true;
        }
        else return false;
    }


    private void StartWallJumpCooldown()
    {
        isWallJumpCooldownActive = false;
        wallJumpTimer = wallJumpCooldown;
        Debug.Log("Wall jump cooldown started. Cooldown duration: " + wallJumpCooldown);
    }
}
