using UnityEngine;

[System.Serializable]
public enum Direction
{
    Left,
    Right
}

public class PlatformEnemySetAngle : MonoBehaviour
{
    public Direction direction;
    public float moveSpeed = 2f;
    public float rotateAngle = 90f; // Angle to rotate when not grounded
    public float raycastDistance = 0.1f;
    public float raycastOffset = 0.05f; // Offset to prevent raycast from hitting own collider

    private bool hasRotated = false; // Flag to track if rotation has occurred

    private void Start()
    {
        if (direction == Direction.Right) rotateAngle = -90;
        else rotateAngle = 90;
    }
    void Update()
    {


        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up * raycastOffset, -transform.up, raycastDistance);
        Debug.DrawRay(transform.position + transform.up * raycastOffset, -transform.up * raycastDistance, Color.green);

        if (hit.collider != null)
        {

            hasRotated = false;

            MoveEnemy();
        }
        else
        {
            if (!hasRotated)
            {
                transform.Rotate(Vector3.forward * rotateAngle);
                hasRotated = true;
            }

            MoveEnemy();
        }
    }

    private void MoveEnemy()
    {
        if (direction == Direction.Right)transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        if (direction == Direction.Left) transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}
