using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputManager : MonoBehaviour
{
    private IUIInput _UIInput;

    public PauseMenu pauseMenu;
    void Update()
    {
        if (!PauseController.Instance.IsPaused)
        {
            if (_UIInput == null)
                _UIInput = GetComponent<IUIInput>();
            
            print(_UIInput.IsTogglePauseDown());
            if (_UIInput != null)
            {
                if (_UIInput.IsTogglePauseDown())
                {
                    print("Pause Pressed");
                    if (!pauseMenu)
                        pauseMenu = GetComponentInChildren<PauseMenu>(true);

                    pauseMenu?.Show();
                }
            }
        }
        
    }
}
