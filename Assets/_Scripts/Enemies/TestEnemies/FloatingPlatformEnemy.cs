using UnityEngine;

public class FloatingPlatformEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotateSpeed = 100f;
    public float raycastDistance = 0.1f;
    public float raycastOffset = 0.05f; // Offset to prevent raycast from hitting own collider

    public bool isGrounded = false;

    void Update()
    {
        // Perform the raycast to check if grounded
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up * raycastOffset, -transform.up, raycastDistance);
        Debug.DrawRay(transform.position + transform.up * raycastOffset, -transform.up * raycastDistance, Color.green);

        // If the raycast hits something, consider the enemy grounded
        if (hit.collider != null)
        {
            isGrounded = true;

            // Move the enemy
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            // Get the local down direction
            Vector2 localDown = -transform.up;

            // If the local down is pointing upward, reverse the direction
            if (localDown.y > 0)
            {
                localDown = -localDown;
            }

            // Move the enemy downward along its local Y axis
            transform.Translate(localDown * Time.deltaTime, Space.Self);

            // Rotate the enemy
            transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);

            isGrounded = false;
        }
    }
}
