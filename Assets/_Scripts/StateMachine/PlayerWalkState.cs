using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerState idleState;
    public PlayerState jumpState;
    public PlayerState attackState;

    public float speed = 2f;

    public TriggerStringSO animationTrigger;
    public float transitionDuration = 0.3f;

    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);
        stateMachine.PlayerAnimator.CrossFade(animationTrigger.triggerString, transitionDuration);
    }

    public override void UpdateState()
    {
        if (stateMachine.PlayerInput.isJumpPressed())
        {
            stateMachine.TransitionToState(jumpState);
        }
        else
        {
            float horizontalInput = stateMachine.PlayerInput.GetPrimaryAxis().x;
            stateMachine.PlayerRigidBody.velocity = new Vector2(horizontalInput * speed, stateMachine.PlayerRigidBody.velocity.y);

            if (horizontalInput > 0)
            {
                stateMachine.Player.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontalInput < 0)
            {
                stateMachine.Player.localScale = new Vector3(-1, 1, 1);
            }

            if (Mathf.Abs(horizontalInput) > 0)
            {
                stateMachine.TransitionToState(this);
            }
            else
            {
                stateMachine.TransitionToState(idleState);
            }
        }
    }
}
