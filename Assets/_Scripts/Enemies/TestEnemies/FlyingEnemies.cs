using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float amplitude = 1f; // Amplitude of the sine wave
    public float frequency = 1f; // Frequency of the sine wave

    [Header("")]
    [Space()]
    public float raycastDistance = 1f;
    public Vector2 raycastOffset;

    private Vector2 startPosition;
    private bool movingRight = true;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Determine the current movement direction
        Vector2 currentDirection = movingRight ? Vector2.right : Vector2.left;

        // Cast a ray to detect obstacles in the path
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + currentDirection * raycastOffset, currentDirection, raycastDistance);

        // Draw the ray for visualization
        Debug.DrawRay((Vector2)transform.position + currentDirection * raycastOffset, currentDirection * raycastDistance, Color.green);

        // Calculate horizontal movement
        float horizontalMovement = moveSpeed * Time.deltaTime * (movingRight ? 1f : -1f);
        startPosition.x += horizontalMovement;

        // Calculate vertical offset using sine wave
        float verticalOffset = amplitude * Mathf.Sin(Time.time * frequency);

        // Calculate new position
        Vector2 newPosition = new Vector2(startPosition.x, startPosition.y + verticalOffset);

        // Check if the raycast hits anything
        if (hit.collider == null)
        {
            // Move to the new position if there's no obstacle
            transform.position = newPosition;
        }
        else
        {
            // Change direction if obstacle is detected
            movingRight = !movingRight;
        }
    }
}
