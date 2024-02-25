using UnityEngine;

public class DamageTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(); // Call the Damage method on the IDamageable component
        }
    }
}
