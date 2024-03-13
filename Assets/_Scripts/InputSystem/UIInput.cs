using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class UIInput : MonoBehaviour, IUIInput
{
    private PlayerInputProcessor m_TogglePauseP = new PlayerInputProcessor();
    private PlayerInputProcessor m_SubmitP = new PlayerInputProcessor();

    private void Awake()
    {
        MessageBuffer<GameUnPausedMessage>.Subscribe(OnGameUnpaused);
    }

    void OnGameUnpaused(GameUnPausedMessage msg)
    {
        Flush();
    }

    private void OnSubmit(InputValue value)
    {
        m_SubmitP.Process(value);
    }

    private void OnPause(InputValue value)
    {
        m_TogglePauseP.Process(value);
    }

    public bool IsTogglePauseDown()
    {
        return m_TogglePauseP.IsDown();
    }
    public bool IsSubmitHeld()
    {
        return m_SubmitP.IsHeld();
    }

    public bool IsSubmitDown()
    {
        return m_SubmitP.IsDown();
    }

    public bool IsSubmitUp()
    {
        return m_SubmitP.IsUp();
    }
    private void Flush()
    {
        m_TogglePauseP.Clear();
        m_SubmitP.Clear();
    }


}
