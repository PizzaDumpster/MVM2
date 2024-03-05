using UnityEngine;

public class ParabolaFollower : MonoBehaviour
{
    public float maxHeight = 5f; // Maximum height of the parabola
    public float distance = 10f; // Horizontal distance covered by the parabola
    public int resolution = 50; // Number of line segments to draw the parabola
    public float speed = 5f; // Speed of the object

    public Vector3[] parabolaPoints;
    private int currentIndex = 0;
    public bool movingForward = true; // Flag to track movement direction
    public Transform endTransform;

    public SpriteRenderer childSpriteRenderer;

    void Start()
    {
        SetDistanceFromEndTransform();
        CalculateParabola();
    }
    public void OnValidate()
    {
        SetDistanceFromEndTransform();
        CalculateParabola();
    }

    void CalculateParabola()
    {
        parabolaPoints = new Vector3[resolution + 1];

        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            float x = t * distance;
            float y = -maxHeight * Mathf.Pow((x / distance) - 0.5f, 2) + maxHeight; // Adjusted parabola equation
            parabolaPoints[i] = transform.position + new Vector3(x, y, 0);
        }

        // Adjust the starting point of the parabola
        Vector3 offset = transform.position - parabolaPoints[0];
        for (int i = 0; i <= resolution; i++)
        {
            parabolaPoints[i] += offset;
        }
    }

    void Update()
    {
        MoveObject();
    }

    void MoveObject()
    {
        Vector3 targetPosition = parabolaPoints[currentIndex];

        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        transform.position += moveDirection * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Move to the next point in the parabola
            if (movingForward)
            {
                currentIndex++;
                if (currentIndex >= parabolaPoints.Length)
                {
                    currentIndex = parabolaPoints.Length - 1;
                    movingForward = false;
                }
            }
            else
            {
                currentIndex--;
                if (currentIndex < 0)
                {
                    currentIndex = 0;
                    movingForward = true;
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

    void SetDistanceFromEndTransform()
    {
        if (endTransform != null)
        {
            distance = Vector3.Distance(transform.position, endTransform.position);
        }
    }

    private void OnDrawGizmos()
    {
        if (parabolaPoints == null || parabolaPoints.Length == 0)
            return;

        Gizmos.color = Color.red;

        for (int i = 0; i < parabolaPoints.Length - 1; i++)
        {
            Gizmos.DrawLine(parabolaPoints[i], parabolaPoints[i + 1]);
        }
    }
}
