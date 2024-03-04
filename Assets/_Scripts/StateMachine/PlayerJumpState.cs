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


    [Header("Power Up")]
    public PowerUpSO doubleJump;
    public PowerUpSO wallslide;
    public PowerUpSO dash;

    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        stateMachine.PlayerRigidBody.sharedMaterial = noStick;

        jumpBufferCounter = jumpBufferTime;

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

        if (stateMachine.PlayerInput.IsDashPressed() && stateMachine.CanDash() && stateMachine.unlockedAbilities.Contains(dash))
        {
            stateMachine.TransitionToState(dashState);
        }

        if (stateMachine.IsWalled() && !stateMachine.GroundCheck.IsGrounded() && stateMachine.PlayerRigidBody.velocity.y < -0.1f && stateMachine.unlockedAbilities.Contains(wallslide))
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
        float horizontalInput = stateMachine.PlayerInput.GetPrimaryAxis().x;

        // Check if the horizontal input direction is opposite to the player's velocity direction
        bool isOppositeDirection = Mathf.Sign(horizontalInput) != Mathf.Sign(stateMachine.PlayerRigidBody.velocity.x);

        if (jumpBufferCounter > 0 && isOppositeDirection)
        {
            // Jump opposite to the input direction
            stateMachine.PlayerRigidBody.velocity = new Vector2(-horizontalInput * jumpForce, jumpForce);
        }
        else if (jumpBufferCounter > 0 && stateMachine.GroundCheck.IsGrounded())
        {
            stateMachine.PlayerRigidBody.velocity = new Vector2(stateMachine.PlayerRigidBody.velocity.x, jumpForce);
        }
        else if (jumpBufferCounter > 0 && !stateMachine.IsWalled())
        {
            stateMachine.PlayerRigidBody.velocity = new Vector2(stateMachine.PlayerRigidBody.velocity.x, jumpForce);
        }
        else if (stateMachine.PlayerInput.IsJumpPressed() && (jumpCounter < maxJumps || canDoubleJump) && !stateMachine.GroundCheck.IsGrounded() && stateMachine.unlockedAbilities.Contains(doubleJump))
        {
            jumpCounter++;
            stateMachine.PlayerRigidBody.velocity = new Vector2(stateMachine.PlayerRigidBody.velocity.x, doubleJumpForce);
            canDoubleJump = false;
        }
    }

}
