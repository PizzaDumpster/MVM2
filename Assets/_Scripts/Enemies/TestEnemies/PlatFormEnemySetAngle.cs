using UnityEngine;

public class PlatformEnemySetAngle : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotateAngle = 90f; // Angle to rotate when not grounded
    public float raycastDistance = 0.1f;
    public float raycastOffset = 0.05f; // Offset to prevent raycast from hitting own collider

    private bool hasRotated = false; // Flag to track if rotation has occurred

    void Update()
    {
        // Perform the raycast to check if grounded
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up * raycastOffset, -transform.up, raycastDistance);
        Debug.DrawRay(transform.position + transform.up * raycastOffset, -transform.up * raycastDistance, Color.green);

        // If the raycast hits something, consider the enemy grounded
        if (hit.collider != null)
        {
            // Reset the flag when grounded
            hasRotated = false;

            // Move the enemy
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            // If not grounded and hasn't rotated yet
            if (!hasRotated)
            {
                // Rotate the enemy 90 degrees forward
                transform.Rotate(Vector3.forward * rotateAngle);
                hasRotated = true; // Set the flag to true
            }

            // Move the enemy
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }
}
