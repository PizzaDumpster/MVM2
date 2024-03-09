using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadDetection : MonoBehaviour
{
    void Start()
    {
        // Check if any joysticks are connected
        string[] joystickNames = Input.GetJoystickNames();
        if (joystickNames.Length > 0)
        {
            Debug.Log("A controller is connected.");
        }
        else
        {
            Debug.Log("No controller detected.");
        }
    }
}
