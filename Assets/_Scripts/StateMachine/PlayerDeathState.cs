using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{

    public PlayerState idleState;

    public TriggerStringSO animationTrigger;
    public float transitionDuration = 0.0f;

    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        stateMachine.PlayerAnimator.CrossFade(animationTrigger.triggerString, transitionDuration);
    }
    public override void UpdateState()
    {
        
    }
}
