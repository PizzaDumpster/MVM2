using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float raycastDistance = 0.1f;

    private bool movingRight = true;
    public Transform groundCheck;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, raycastDistance);
        Debug.DrawRay(groundCheck.position, Vector2.down * raycastDistance, Color.green);

        if (hit.collider == null)
        {
            movingRight = !movingRight;
            Debug.Log("Changing direction!");
        }

        float movementDirection = movingRight ? 1 : -1;
        float movementDistance = moveSpeed * Time.deltaTime;

        Vector3 movement = Vector3.right * movementDirection * movementDistance;
        transform.Translate(movement);

        if ((movingRight && transform.localScale.x < 0) || (!movingRight && transform.localScale.x > 0))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
