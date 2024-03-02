using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    protected PlayerStateMachine stateMachine;

    public virtual void EnterState(PlayerStateMachine stateMachine) { this.stateMachine = stateMachine; }
    public abstract void UpdateState();
}