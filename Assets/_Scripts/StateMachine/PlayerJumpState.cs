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

        stateMachine.PlayerAnimator.CrossFade(animationTrigger.triggerString, transitionDuration);
    }

    public override void UpdateState()
    {
        jumpBufferCounter -= Time.deltaTime;

        if (stateMachine.GroundCheck.IsGrounded())
        {
            jumpCounter = 0; 
            canDoubleJump = true;
        }

        if (jumpBufferCounter > 0)
        {
            stateMachine.PlayerRigidBody.velocity = new Vector2(stateMachine.PlayerRigidBody.velocity.x, jumpForce);
        }
        else if (stateMachine.PlayerInput.IsJumpPressed() && (jumpCounter < maxJumps || canDoubleJump))
        {
            jumpCounter++;
            stateMachine.PlayerRigidBody.velocity = new Vector2(stateMachine.PlayerRigidBody.velocity.x, doubleJumpForce);
            canDoubleJump = false;
        }

        if (!stateMachine.GroundCheck.IsGrounded() && stateMachine.PlayerInput.IsAttackPressed())
        {
            stateMachine.TransitionToState(attackState);
        }

        if (stateMachine.GroundCheck.IsGrounded() && jumpBufferCounter <= 0)
        {
            stateMachine.TransitionToState(idleState);
        }

        if(stateMachine.PlayerInput.IsDashPressed() && stateMachine.CanDash())
        {
            stateMachine.TransitionToState(dashState);
        }
    }
}
