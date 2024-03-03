using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerState idleState;
    public PlayerState fallsState;
    public PlayerState wallSlideState;

    [Header("Gravity Modifiers")]
    [SerializeField] float initialGravityMultiplier;

    [Header("Dash Variables")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashTime = 0.5f;
    [SerializeField] float noGravityMultiplier = 0f;
    [SerializeField] bool isDashing = false;

    [Header("")]
    public TriggerStringSO animationTrigger;
    public float transitionDuration = 0.0f;

    [Header("")]
    public TrailRenderer trailRenderer;
    public float coolDownTime = 3f;

    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        trailRenderer = GetComponent<TrailRenderer>();

        initialGravityMultiplier = stateMachine.PlayerRigidBody.gravityScale;

        stateMachine.PlayerAnimator.CrossFade(animationTrigger.triggerString, transitionDuration);

        stateMachine.StartCoroutine(Dash());
    }

    public override void UpdateState()
    {

        if (!isDashing && stateMachine.GroundCheck.IsGrounded())
        {
            stateMachine.TransitionToState(idleState);
        }

        if (!isDashing && !stateMachine.GroundCheck.IsGrounded())
        {
            stateMachine.TransitionToState(fallsState);
        }

        if (stateMachine.IsWalled())
        {
            stateMachine.TransitionToState(wallSlideState);
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;

        stateMachine.PlayerRigidBody.gravityScale = noGravityMultiplier;

        Vector2 dashDirection = new Vector2(stateMachine.PlayerInput.GetPrimaryAxis().x, stateMachine.PlayerInput.GetPrimaryAxis().y).normalized;

        stateMachine.PlayerRigidBody.velocity = dashDirection * dashSpeed;
        trailRenderer.emitting = true;

        yield return new WaitForSeconds(dashTime);

        trailRenderer.emitting = false;
        stateMachine.PlayerRigidBody.velocity = Vector2.zero;
        stateMachine.PlayerRigidBody.gravityScale = initialGravityMultiplier;
        isDashing = false;
        StartDashCooldown();
    }

    private void StartDashCooldown()
    {
        stateMachine.StartDashCooldown(coolDownTime);
    }

    public override bool CanDash()
    {
        return true;
    }
}
