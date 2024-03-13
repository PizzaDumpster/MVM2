
using UnityEngine;

public interface IPlayerInput
{
    Vector2 GetPrimaryAxis();

    bool IsJumpPressed();

    bool IsJumpHeld();

    bool IsAttackPressed();

    bool IsDashPressed();

    bool IsDashHeld();

    bool IsJumpUp();
}
