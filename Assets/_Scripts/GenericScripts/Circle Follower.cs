using UnityEngine;

public class CircleFollower : MonoBehaviour
{
    public float radius = 5f; // Radius of the circle
    public int resolution = 50; // Number of points on the circle
    public float speed = 5f; // Speed of the object

    public Vector3[] circlePoints;
    private int currentIndex = 0;
    public bool movingForward = true; // Flag to track movement direction

    public SpriteRenderer childSpriteRenderer;

    void Start()
    {
        CalculateCircle();
    }

    public void OnValidate()
    {
        CalculateCircle();
    }

    void CalculateCircle()
    {
        circlePoints = new Vector3[resolution + 1];

        for (int i = 0; i <= resolution; i++)
        {
            float angle = i * 2 * Mathf.PI / resolution;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            circlePoints[i] = transform.position + new Vector3(x, y, 0);
        }
    }

    void Update()
    {
        MoveObject();
    }

    void MoveObject()
    {
        Vector3 targetPosition = circlePoints[currentIndex];

        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        transform.position += moveDirection * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Move to the next point on the circle
            if (movingForward)
            {
                currentIndex++;
                if (currentIndex >= circlePoints.Length)
                {
                    currentIndex = 0;
                }
            }
            else
            {
                currentIndex--;
                if (currentIndex < 0)
                {
                    currentIndex = circlePoints.Length - 1;
                }
            }

            if (childSpriteRenderer != null)
            {
                childSpriteRenderer.flipX = movingForward;
            }
            else
            {
                Debug.LogWarning("Child Sprite Renderer not assigned.");
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (circlePoints == null || circlePoints.Length == 0)
            return;

        Gizmos.color = Color.blue;

        for (int i = 0; i < circlePoints.Length - 1; i++)
        {
            Gizmos.DrawLine(circlePoints[i], circlePoints[i + 1]);
        }
    }
}
