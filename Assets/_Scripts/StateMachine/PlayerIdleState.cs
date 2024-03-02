using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public float movementThreshold = 0.1f;


    public TriggerStringSO animationTrigger;
    public float transitionDuration = 0.3f;

    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        stateMachine.PlayerAnimator.CrossFade(animationTrigger.triggerString, transitionDuration);
    }

    public override void UpdateState()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    stateMachine.TransitionToState(stateMachine.attackState);
        //}

        //if (stateMachine.Movement(movementThreshold) > 0)
        //{
        //    stateMachine.TransitionToState(stateMachine.movementState);
        //}
    }
}