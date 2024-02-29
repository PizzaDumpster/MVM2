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

    private void OnSubmit(InputAction value)
    {

    }

    private void OnPause(InputValue value)
    {
        m_SubmitP.Process(value);
    }

    public bool IsTogglePauseDown()
    {
        return m_TogglePauseP.IsDown();
    }
    public bool IsSubmitDown()
    {
        return m_SubmitP.IsDown();
    }
    private void Flush()
    {
        m_TogglePauseP.Clear();
        m_SubmitP.Clear();
    }


}
