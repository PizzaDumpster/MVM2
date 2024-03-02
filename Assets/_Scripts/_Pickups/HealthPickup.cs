using UnityEngine;

public class HealthPickup : PickUp
{
    [Header("")]
    [SerializeField] int healthAmount;

    private PlayerRestoreHealth restoreHealth = new PlayerRestoreHealth();

    public override void Start()
    {
        base.Start();
        restoreHealth.restoreAmount = healthAmount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerObject.objectString))
        {
            AudioPlayer.Instance.PlayAudioClip(pickupClip);
            MessageBuffer<PlayerRestoreHealth>.Dispatch(restoreHealth);
            this.gameObject.SetActive(false);
        }
    }
}
