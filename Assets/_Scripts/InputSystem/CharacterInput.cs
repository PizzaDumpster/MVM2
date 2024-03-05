using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour, IPlayerInput
{
    private Vector2 m_InputAxis;
    private PlayerInputProcessor m_JumpP = new PlayerInputProcessor();
    private PlayerInputProcessor m_AttackP = new PlayerInputProcessor();
    private PlayerInputProcessor m_DashP = new PlayerInputProcessor();


    private void OnPrimaryAxis(InputValue value)
    {
        m_InputAxis = value.Get<Vector2>();
    }
    private void OnAttack(InputValue value)
    {
        m_AttackP.Process(value);
    }

    private void OnJump(InputValue value)
    {
        m_JumpP.Process(value);
    }

    private void OnDash(InputValue value)
    {
        m_DashP.Process(value);

    }

    //////////////////////////////////////
    
    public Vector2 GetPrimaryAxis()
    {
        return m_InputAxis;
    }

    public bool IsAttackPressed()
    {
        return m_AttackP.IsDown();
    }

    public bool IsDashPressed()
    {
        return m_DashP.IsDown();
    }

    public bool IsJumpHeld()
    {
        return m_JumpP.IsHeld();
    }

    public bool IsJumpPressed()
    {
        return m_JumpP.IsDown();
    }
    public bool IsJumpUp()
    {
        return m_JumpP.IsUp();
    }

    private void Flush()
    {
        m_JumpP.Clear();
        m_DashP.Clear();
        m_AttackP.Clear();
    }


}
