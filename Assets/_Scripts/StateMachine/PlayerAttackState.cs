using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerState idleState;
    public PlayerState jumpState;

    public TriggerStringSO animationTrigger;
    private bool animationFinished = false;

    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        stateMachine.PlayerAnimator.CrossFade(animationTrigger.triggerString, 0.0f);
        animationFinished = false;
    }

    public override void UpdateState()
    {
        if (!animationFinished && stateMachine.PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            if (stateMachine.GroundCheck.IsGrounded())
            {
                stateMachine.TransitionToState(idleState);
                animationFinished = true;
            }
            else
            {
                stateMachine.TransitionToState(jumpState);
                animationFinished = true;
            }

        }
    }
}
