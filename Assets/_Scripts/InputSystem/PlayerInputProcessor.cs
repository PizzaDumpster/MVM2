using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputProcessor
{

    public int FrameDown;
    public int FrameUp;

    public bool IsDown() { return FrameDown == Time.frameCount; }
    public bool IsUp() { return FrameUp == Time.frameCount; }
    public bool IsHeld() { return FrameDown != -1 && FrameUp == -1; }

    public void Process(InputValue value)
    {
        if (IsHeld() && !value.isPressed)
        {
            FrameDown = -1;
            FrameUp = Time.frameCount;
        }
        else if (value.isPressed)
        {
            FrameUp = -1;
            FrameDown = Time.frameCount;
        }
    }

    public void Clear()
    {
        FrameDown = -1;
        FrameUp = -1;
    }
}