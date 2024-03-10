using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionState : PlayerState
{
    public InteractiveDetector interactiveDetector;
    public Interactive interactive;

    [Header("States")]
    public PlayerState idleState;

    [Header("Animations")]
    public TriggerStringSO animationTrigger;
    public float transitionDuration = 0.0f;
    public float interactDelay = 0.0f;
    public float m_InitialDelay = 0.0f; 
    private float m_Time;

    private bool interacted = false;
    

    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        m_Time = 0;
        interacted = false;

        stateMachine.PlayerAnimator.CrossFade(animationTrigger.triggerString, transitionDuration);

        interactive = interactiveDetector.interactiveObject;
        if (interactive == null) stateMachine.TransitionToState(idleState);
    }

    public override void UpdateState()
    {
        m_Time += Time.deltaTime;

        if(!interacted && m_Time > interactDelay)
        {
            Interact();
        }
        else if(m_Time > m_InitialDelay)
        {
            stateMachine.TransitionToState(idleState);
        }


    }

    public override void ExitState()
    {
        
    }

    public void Interact()
    {
        interactive.Interact();
        interacted = true;
    }

}
