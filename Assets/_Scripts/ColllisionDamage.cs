using UnityEngine;

public class CollisionDamage : MonoBehaviour
{

    public int damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with player detected.");

            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                Debug.Log("IDamageable component found on player.");
                damageable.Damage(damage);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.LogWarning("IDamageable component not found on player.");
            }
        }
    }
}
