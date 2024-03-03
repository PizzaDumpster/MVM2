using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    protected PlayerStateMachine stateMachine;

    public virtual void EnterState(PlayerStateMachine stateMachine) { this.stateMachine = stateMachine; }
    public abstract void UpdateState();

    public virtual bool CanDash()
    {
        return false; // By default, assume that the player can always dash
    }

    public virtual bool CanWallJump()
    {
        return false; // By default, assume that the player can always dash
    }

}