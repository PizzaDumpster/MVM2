using UnityEngine;

public class OvalFollower : MonoBehaviour
{
    public float radiusX = 5f; // Radius of the oval along the x-axis
    public float radiusY = 3f; // Radius of the oval along the y-axis
    public int resolution = 50; // Number of points on the oval
    public float speed = 5f; // Speed of the object

    public Vector3[] ovalPoints;
    private int currentIndex = 0;
    public bool movingForward = true; // Flag to track movement direction

    public SpriteRenderer childSpriteRenderer;

    void Start()
    {
        CalculateOval();
    }

    public void OnValidate()
    {
        CalculateOval();
    }

    void CalculateOval()
    {
        ovalPoints = new Vector3[resolution + 1];

        for (int i = 0; i <= resolution; i++)
        {
            float angle = i * 2 * Mathf.PI / resolution;
            float x = Mathf.Cos(angle) * radiusX;
            float y = Mathf.Sin(angle) * radiusY;
            ovalPoints[i] = transform.position + new Vector3(x, y, 0);
        }
    }

    void Update()
    {
        MoveObject();
    }

    void MoveObject()
    {
        Vector3 targetPosition = ovalPoints[currentIndex];

        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        transform.position += moveDirection * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Move to the next point on the oval
            if (movingForward)
            {
                currentIndex++;
                if (currentIndex >= ovalPoints.Length)
                {
                    currentIndex = 0;
                }
            }
            else
            {
                currentIndex--;
                if (currentIndex < 0)
                {
                    currentIndex = ovalPoints.Length - 1;
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
        if (ovalPoints == null || ovalPoints.Length == 0)
            return;

        Gizmos.color = Color.blue;

        for (int i = 0; i < ovalPoints.Length - 1; i++)
        {
            Gizmos.DrawLine(ovalPoints[i], ovalPoints[i + 1]);
        }
    }
}
