using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMeter : BaseMessage { public float DashAmount; }
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

    DashMeter dashAmount = new DashMeter();

    [Header("")]
    public TriggerStringSO animationTrigger;
    public float transitionDuration = 0.0f;

    [Header("")]
    public TrailRenderer trailRenderer;
    public float coolDownTime = 3f;

    [Header("Power Up")]
    public PowerUpSO wallSlide;

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
            stateMachine.PlayerRigidBody.gravityScale = initialGravityMultiplier;
            stateMachine.TransitionToState(idleState);
        }

        if (!isDashing && !stateMachine.GroundCheck.IsGrounded())
        {
            stateMachine.PlayerRigidBody.gravityScale = initialGravityMultiplier;
            stateMachine.TransitionToState(fallsState);
        }

        if (stateMachine.WallCheck.IsWalled() && stateMachine.unlockedAbilities.Contains(wallSlide))
        {
            stateMachine.TransitionToState(wallSlideState);
        }



    }

    private IEnumerator Dash()
    {
        isDashing = true;

        stateMachine.PlayerRigidBody.gravityScale = noGravityMultiplier;

        Vector2 dashDirection;

        if (Mathf.Approximately(stateMachine.PlayerInput.GetPrimaryAxis().sqrMagnitude, 0f))
        {
            dashDirection = new Vector2(stateMachine.Player.localScale.x, 0f);
        }
        else
        {
            dashDirection = new Vector2(stateMachine.PlayerInput.GetPrimaryAxis().x, stateMachine.PlayerInput.GetPrimaryAxis().y).normalized;
        }

        stateMachine.PlayerRigidBody.velocity = dashDirection * dashSpeed;
        trailRenderer.emitting = true;

        float elapsedTime = 0f;

        while (elapsedTime < dashTime)
        {
            float timePercentage = elapsedTime / dashTime;

            dashAmount.DashAmount = Mathf.Lerp(100f, 0f, timePercentage);

            MessageBuffer<DashMeter>.Dispatch(dashAmount);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        dashAmount.DashAmount = 0;
        MessageBuffer<DashMeter>.Dispatch(dashAmount);

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
