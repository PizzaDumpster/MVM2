using UnityEngine;

public class ParabolaDrawer : MonoBehaviour
{
    public float maxHeight = 5f; // Maximum height of the parabola
    public float distance = 10f; // Horizontal distance covered by the parabola
    public int resolution = 50; // Number of line segments to draw the parabola

    private void OnDrawGizmos()
    {
        DrawParabola(transform.position, maxHeight, distance, resolution);
    }

    void DrawParabola(Vector3 startPos, float height, float length, int segments)
    {
        Vector3 lastPoint = startPos;

        for (int i = 1; i <= segments; i++)
        {
            float t = i / (float)segments;
            float x = t * length;
            float y = -height * Mathf.Pow(x / length - 0.5f, 2) + height;

            Vector3 nextPoint = startPos + new Vector3(x, y, 0);
            Debug.DrawLine(lastPoint, nextPoint, Color.red);
            lastPoint = nextPoint;
        }
    }
}
