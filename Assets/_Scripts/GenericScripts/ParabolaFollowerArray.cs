using UnityEngine;
using System.Collections.Generic;

public class ParabolaFollowerArray : MonoBehaviour
{
    [System.Serializable]
    public class ParabolaData
    {
        public float maxHeight = 5f; // Maximum height of the parabola
        public float distance = 10f; // Horizontal distance covered by the parabola
        public int resolution = 50; // Number of line segments to draw the parabola
        public bool movingForward = true; // Flag to track movement direction
        [HideInInspector] public Vector3 startPosition; // Start position of the parabola
    }

    public List<ParabolaData> parabolas = new List<ParabolaData>(); // List of parabolas

    [SerializeField]
    public List<Vector3[]> parabolaPointsList = new List<Vector3[]>(); // List of parabola points

    public List<Vector3> allParabolaPoints;
    private int currentIndex = 0;
    public float speed = 5f; // Speed of the object
    public SpriteRenderer childSpriteRenderer;
    private bool movingForward = true; // Flag to track movement direction

    void Start()
    {
        SetStartPositions();
        CalculateParabolas();
    }

    public void OnValidate()
    {
        SetStartPositions();
        CalculateParabolas();
    }

    void SetStartPositions()
    {
        // Set the start positions of the parabolas
        if (parabolas.Count > 0)
        {
            parabolas[0].startPosition = transform.position;
            for (int i = 1; i < parabolas.Count; i++)
            {
                parabolas[i].startPosition = parabolas[i - 1].startPosition + new Vector3(parabolas[i - 1].distance, 0f, 0f);
            }
        }
    }

    void CalculateParabolas()
    {
        allParabolaPoints.Clear(); // Clear existing parabola points


        foreach (ParabolaData parabola in parabolas)
        {
            Vector3[] parabolaPoints = new Vector3[parabola.resolution + 1];

            for (int i = 0; i <= parabola.resolution; i++)
            {
                float t = i / (float)parabola.resolution;
                float x = t * parabola.distance;
                float y = -parabola.maxHeight * Mathf.Pow((x / parabola.distance) - 0.5f, 2) + parabola.maxHeight; // Adjusted parabola equation
                parabolaPoints[i] = parabola.startPosition + new Vector3(x, y, 0);
            }

            // Append the parabola points to the list
            allParabolaPoints.AddRange(parabolaPoints);
        }

        // Calculate the offset and adjust all parabola points
        Vector3 offset = transform.position - allParabolaPoints[0];
        for (int i = 0; i < allParabolaPoints.Count; i++)
        {
            allParabolaPoints[i] += offset;
        }
    }



    void Update()
    {
        MoveObject();
    }

    void MoveObject()
    {
        Vector3 targetPosition = allParabolaPoints[currentIndex];

        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        transform.position += moveDirection * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Move to the next point in the parabola
            if (movingForward)
            {
                currentIndex++;
                if (currentIndex >= allParabolaPoints.Count)
                {
                    currentIndex = allParabolaPoints.Count - 1;
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

    private void OnDrawGizmos()
    {
        if (allParabolaPoints == null || allParabolaPoints.Count == 0)
            return;

        Gizmos.color = Color.red;

        for (int i = 0; i < allParabolaPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(allParabolaPoints[i], allParabolaPoints[i + 1]);
        }
    }
}
