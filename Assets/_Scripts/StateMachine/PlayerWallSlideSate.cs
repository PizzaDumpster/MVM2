using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideSate : PlayerState
{
    [Header("States")]
    public PlayerState fallState;
    public PlayerState idleState;

    [Header("Wall Jumping Variables")]
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float outwardJumpForce = 15f;
    [SerializeField] float wallJumpingTime = 1f;

    [Header("Animation")]
    public TriggerStringSO animationTrigger;
    public TriggerStringSO wallJumpTrigger;
    public float transitionDuration = 0.0f;

    private float wallJumpingCounter = 0;
    private bool isJumping = false;

    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        wallJumpingCounter = 0;
        isJumping = false;

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
            return;
        }

        if (stateMachine.PlayerInput.IsJumpPressed())
        {
            WallJump();
        }

        if (isJumping)
        {
            wallJumpingCounter -= Time.deltaTime;
            if (wallJumpingCounter <= 0)
            {
                stateMachine.TransitionToState(fallState);
            }
        }
    }

    private void WallJump()
    {
        if (stateMachine.IsWalled() && !stateMachine.GroundCheck.IsGrounded())
        {
            stateMachine.PlayerAnimator.CrossFade(wallJumpTrigger.triggerString, transitionDuration);
            wallJumpingCounter = wallJumpingTime;
            float outwardJumpDirection = -stateMachine.Player.localScale.x;
            stateMachine.PlayerRigidBody.velocity = new Vector2(outwardJumpDirection * outwardJumpForce, jumpForce);
            if (stateMachine.Player.localScale.x != outwardJumpDirection)
            {
                Vector3 localScale = stateMachine.Player.localScale;
                localScale.x *= -1;
                stateMachine.Player.localScale = localScale;
            }
            isJumping = true;
        }
    }

    public override bool CanWallJump()
    {
        return true;
    }
}
