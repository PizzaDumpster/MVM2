using UnityEngine;

public class EnemyPingPong : MonoBehaviour
{
    public Vector2 raycastOffset;
    public float raycastDistance = 1f; // Define raycast distance
    public float speed = 1f; // Speed for moving the enemy
    private Vector2 initialPosition;
    private bool movingRight = true;
    public SpriteRenderer childSpriteRenderer;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + direction * raycastOffset, direction, raycastDistance);

        Debug.DrawRay((Vector2)transform.position + direction * raycastOffset, direction * raycastDistance, Color.green);

        if (hit.collider == null)
        {
            // Move only when hit nothing
            transform.Translate(direction * speed * Time.deltaTime);
            childSpriteRenderer.flipX = movingRight;
        }
        else
        {
            // Change direction when hit something
            movingRight = !movingRight;
        }
    }
}
