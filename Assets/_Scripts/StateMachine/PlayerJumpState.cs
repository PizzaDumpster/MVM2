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
    [SerializeField] float airTimer = 0.0f;

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
        if(jumpBufferCounter > 0)
        {
            stateMachine.PlayerRigidBody.velocity = new Vector2(stateMachine.PlayerRigidBody.velocity.x, jumpForce);
        }

        if(stateMachine.GroundCheck.IsGrounded() && stateMachine.PlayerInput.IsAttackPressed())
        {
            stateMachine.TransitionToState(attackState);
        }

        if (stateMachine.GroundCheck.IsGrounded() && jumpBufferCounter <= 0)
        {
            stateMachine.TransitionToState(idleState);
        }

    }
}
