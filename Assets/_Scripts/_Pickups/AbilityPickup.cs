using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUpPowerUp : BaseMessage { public PowerUpSO powerUp; }

public class AbilityPickup : PickUp
{
    [Header("")]
    public PowerUpSO powerUp;
    private PickedUpPowerUp pickedUpPower = new PickedUpPowerUp();

    public override void Start()
    {
        base.Start();
        pickedUpPower.powerUp = powerUp;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerObject.objectString))
        {
            AudioPlayer.Instance.PlayAudioClip(pickupClip);
            MessageBuffer<PickedUpPowerUp>.Dispatch(pickedUpPower);
            this.gameObject.SetActive(false);
        }
    }
}
