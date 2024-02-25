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

    public SpriteRenderer childSpriteRenderer;


    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        Vector2 currentDirection = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + currentDirection * raycastOffset, currentDirection, raycastDistance);

        Debug.DrawRay((Vector2)transform.position + currentDirection * raycastOffset, currentDirection * raycastDistance, Color.green);

        float horizontalMovement = moveSpeed * Time.deltaTime * (movingRight ? 1f : -1f);
        startPosition.x += horizontalMovement;

        float verticalOffset = amplitude * Mathf.Sin(Time.time * frequency);

        Vector2 newPosition = new Vector2(startPosition.x, startPosition.y + verticalOffset);

        if (hit.collider == null)
        {
            transform.position = newPosition;
            childSpriteRenderer.flipX = movingRight;
        }
        else
        {
            movingRight = !movingRight;
        }
    }
}
