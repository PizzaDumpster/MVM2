
using UnityEngine;

public interface IPlayerInput
{
    Vector2 GetPrimaryAxis();

    bool isJumpPressed();

    bool IsJumpHeld();

    bool IsAttackPressed();

    bool IsDashPressed();
}
