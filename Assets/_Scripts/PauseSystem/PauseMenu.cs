using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private IUIInput m_Input;

    private void Awake()
    {
        m_Input = GetComponentInParent<IUIInput>();
        this.gameObject.SetActive(false);
    }
    void Update()
    {
        if (m_Input.IsTogglePauseDown())
        {
            OnCancel();
        }
    }
    private void OnCancel()
    {
        Hide();
    }

    private void Hide()
    {
        PauseController.Instance.Resume();
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        PauseController.Instance.Pause();
        this.gameObject.SetActive(true);
    }
}
