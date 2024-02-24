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
    private float rotateAngle;

    public float raycastDistance = 0.1f;
    public float raycastOffset = 0.05f;

    private bool hasRotated = false;

    private void Start()
    {
        rotateAngle = (direction == Direction.Right) ? -90f : 90f;
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
        transform.Translate((direction == Direction.Right) ? Vector2.right * moveSpeed * Time.deltaTime : Vector2.left * moveSpeed * Time.deltaTime);
    }
}
