using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    [Header("States")]
    public PlayerState idleState;
    public PlayerState attackState;
    public PlayerState dashState;
    public PlayerState wallSlideState;

    [Header("Jump Variables")]
    [SerializeField] float jumpForce = 0.5f;
    [SerializeField] float jumpBufferTime = 0.2f;
    [SerializeField] float jumpBufferCounter;
    [SerializeField] int jumpCounter = 0;
    [SerializeField] int maxJumps = 1;

    [Header("Wall Jumping Varialbles")]
    [SerializeField] float outwardJumpForce = 15f;
    [SerializeField] float wallJumpingCounter = 0;
    [SerializeField] float wallJumpingTime = 1f;

    [Header("Double Jump Variables")]
    [SerializeField] bool canDoubleJump = false;
    [SerializeField] float doubleJumpForce = 15f;

    [Header("Animation Transitions")]
    public TriggerStringSO animationTrigger;
    public float transitionDuration = 0.0f;

    [Header("Physics Material")]
    [SerializeField] PhysicsMaterial2D noStick;


    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        stateMachine.PlayerRigidBody.sharedMaterial = noStick;

        jumpBufferCounter = jumpBufferTime;
        wallJumpingCounter = wallJumpingTime;

        stateMachine.PlayerAnimator.CrossFade(animationTrigger.triggerString, transitionDuration);
    }

    public override void UpdateState()
    {
        Jump();

        CheckForChange();
    }

    private void CheckForChange()
    {
        if (!stateMachine.GroundCheck.IsGrounded() && stateMachine.PlayerInput.IsAttackPressed())
        {
            stateMachine.TransitionToState(attackState);
        }

        if (stateMachine.GroundCheck.IsGrounded() && jumpBufferCounter <= 0)
        {
            stateMachine.TransitionToState(idleState);
        }

        if (stateMachine.PlayerInput.IsDashPressed() && stateMachine.CanDash())
        {
            stateMachine.TransitionToState(dashState);
        }

        if (stateMachine.IsWalled() && !stateMachine.GroundCheck.IsGrounded() && stateMachine.PlayerRigidBody.velocity.y < -0.1f)
        {
            stateMachine.TransitionToState(wallSlideState);
        }
    }

    private void Jump()
    {
        jumpBufferCounter -= Time.deltaTime;

        if (stateMachine.GroundCheck.IsGrounded())
        {
            jumpCounter = 0;
            canDoubleJump = true;
        }

        if (jumpBufferCounter > 0 && stateMachine.GroundCheck.IsGrounded())
        {
            stateMachine.PlayerRigidBody.velocity = new Vector2(stateMachine.PlayerRigidBody.velocity.x, jumpForce);
        }
        else if (jumpBufferCounter > 0 && !stateMachine.IsWalled())
        {
            stateMachine.PlayerRigidBody.velocity = new Vector2(stateMachine.PlayerRigidBody.velocity.x, jumpForce);
        }
        else if (jumpBufferCounter > 0 && stateMachine.IsWalled() && !stateMachine.GroundCheck.IsGrounded())
        {
            wallJumpingCounter -= Time.deltaTime;
            float outwardJumpDirection = -stateMachine.Player.localScale.x; // Determine the direction away from the wall
            stateMachine.PlayerRigidBody.velocity = new Vector2(outwardJumpDirection * outwardJumpForce, jumpForce);
            if (stateMachine.Player.localScale.x != outwardJumpDirection)
            {
                Vector3 localScale = stateMachine.Player.localScale;
                localScale.x *= -1;
                stateMachine.Player.localScale = localScale;
            }
        }
        else if (stateMachine.PlayerInput.IsJumpPressed() && (jumpCounter < maxJumps || canDoubleJump) && !stateMachine.GroundCheck.IsGrounded())
        {
            jumpCounter++;
            stateMachine.PlayerRigidBody.velocity = new Vector2(stateMachine.PlayerRigidBody.velocity.x, doubleJumpForce);
            canDoubleJump = false;
        }
    }


    public override bool CanWallJump()
    {
        if (wallJumpingCounter <= 0) return false;
        else return true;
    }
}
