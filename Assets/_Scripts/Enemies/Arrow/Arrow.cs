using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float shootForce = 10f;
    public int damageAmount = 10;
    public LayerMask ignoreLayer; // Layer mask to ignore collisions with

    public Rigidbody2D rb;

    public void Shoot()
    {
        Vector2 direction = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));

        rb.AddForce(direction * shootForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EndPoint")) // Check for end point object
        {
            gameObject.SetActive(false);
            return;
        }

        if (ignoreLayer == (ignoreLayer | (1 << collision.gameObject.layer))) // Ignore collisions with specified layer
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return;
        }

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(damageAmount);
        }

        gameObject.SetActive(false);
    }
}
