using UnityEngine;
using UnityEngine.Tilemaps;

public class Arrow : MonoBehaviour
{
    public float shootForce = 10f;
    public int damageAmount = 10;
    

    public Rigidbody2D rb;

    public void Shoot()
    {
        Vector2 direction = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));

        rb.AddForce(direction * shootForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Tilemap collidedTilemap = collision.gameObject.GetComponent<Tilemap>();
        if (collidedTilemap != null)
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
