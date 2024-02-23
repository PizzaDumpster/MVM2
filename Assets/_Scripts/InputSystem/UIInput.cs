using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class UIInput : MonoBehaviour, IUIInput
{
    private PlayerInputProcessor m_TogglePauseP = new PlayerInputProcessor();

    private void Awake()
    {
        MessageBuffer<GameUnPausedMessage>.Subscribe(OnGameUnpaused);
    }

    void OnGameUnpaused(GameUnPausedMessage msg)
    {
        Flush();
    }

    private void OnPause(InputValue value)
    {
        m_TogglePauseP.Process(value);
    }

    public bool IsTogglePauseDown()
    {
        return m_TogglePauseP.IsDown();
    }

    private void Flush()
    {
        m_TogglePauseP.Clear();
    }
}
