using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float raycastDistance = 0.1f;
    public Transform groundCheck; // A child object used to check for ground

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Perform the raycast to check if grounded
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, raycastDistance);
        Debug.DrawRay(groundCheck.position, Vector2.down * raycastDistance, Color.green);

        // If the raycast hits something, consider the enemy grounded
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // Move the enemy
        if (isGrounded)
        {
            if (movingRight)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
        }
        else
        {
            // If not grounded, change direction
            movingRight = !movingRight;
            Debug.Log("Changing direction!");
        }

        // Flip the enemy's sprite if changing direction
        if ((movingRight && transform.localScale.x < 0) || (!movingRight && transform.localScale.x > 0))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        // Debug message for grounded status
        if (isGrounded)
        {
            Debug.Log("Enemy is grounded!");
        }
        else
        {
            Debug.Log("Enemy is not grounded!");
        }
    }
}
