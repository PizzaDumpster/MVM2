using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    protected PlayerStateMachine stateMachine;

    public virtual void EnterState(PlayerStateMachine stateMachine) { this.stateMachine = stateMachine; }
    public abstract void UpdateState();

    public virtual void ExitState() { }

    public virtual bool CanDash()
    {
        return false;
    }

    public virtual bool CanWallJump()
    {
        return false;
    }

}