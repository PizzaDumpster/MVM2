using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private Collider2D collider2d;
    [SerializeField] ObjectStringSO playerObject;

    [Header("")]
    [SerializeField] int healthAmount;

    [Header("")]
    [SerializeField] AudioClip pickupClip;

    private PlayerRestoreHealth restoreHealth = new PlayerRestoreHealth();

    private void Start()
    {
        restoreHealth.restoreAmount = healthAmount;
        collider2d = GetComponent<Collider2D>();
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
