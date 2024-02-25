using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public Collider2D Collider { get; set; }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Collider = other;
        TriggerDamage(Collider);
    }

    public virtual void TriggerDamage(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(); // Call the Damage method on the IDamageable component
        }
    }
}
