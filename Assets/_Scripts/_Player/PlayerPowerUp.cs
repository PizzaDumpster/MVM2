using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    PlayerController playerController;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        MessageBuffer<PickedUpPowerUp>.Subscribe(ActivatePowerUp);
    }

    public void ActivatePowerUp(PickedUpPowerUp powerUp)
    {
        switch (powerUp.powerUp.powerUp)
        {
            case PowerUp.DoubleJump:
                ActivateDoubleJump();
                break;
            case PowerUp.Dash:
                ActivateDash();
                break;
            case PowerUp.WallClimb:
                ActivateWallClimb();
                break;
            case PowerUp.WallSlide:
                ActivateWallSlide();
                break;
            case PowerUp.WallJump:
                ActivateWallJump();
                break;
            default:
                Debug.LogWarning("Unknown power-up type: " + powerUp);
                break;
        }
    }

    void ActivateDoubleJump()
    {
        playerController.unlockedDoubleJump = true;
    }

    void ActivateDash()
    {
        playerController.unlockedDash = true;
    }

    void ActivateWallClimb()
    {
        playerController.wallClimbUnlocked = true;
    }

    void ActivateWallSlide()
    {
        playerController.wallSlidingUnlocked = true;
    }

    void ActivateWallJump()
    {
        playerController.wallJumpingUnlocked = true;
    }
}
