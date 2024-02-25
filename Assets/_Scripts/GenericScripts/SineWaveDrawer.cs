using UnityEngine;

public class SineWaveDrawer : MonoBehaviour
{
    public Transform startPoint;
    public float frequency = 1f; // Adjust this to change the frequency of the wave
    public float amplitude = 1f; // Adjust this to change the amplitude of the wave
    public int numberOfPoints = 100; // Adjust this to change the resolution of the curve
    public float halfWaveWidth = 2f; // Width of half wave

    void Update()
    {
        DrawSineWave();
    }

    void DrawSineWave()
    {
        Vector3 startPos = startPoint.position;

        // Calculate the distance based on half wave width
        float distance = halfWaveWidth * 2f;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i / (float)(numberOfPoints - 1);
            float x = Mathf.Lerp(-halfWaveWidth, halfWaveWidth, t);
            float y = Mathf.Sin(x * Mathf.PI * frequency / halfWaveWidth) * amplitude;
            Vector3 pointOnLine = startPos + transform.right * x + transform.up * y;

            // Calculate the next point on the wave
            float nextT = (i + 1) / (float)(numberOfPoints - 1);
            float nextX = Mathf.Lerp(-halfWaveWidth, halfWaveWidth, nextT);
            float nextY = Mathf.Sin(nextX * Mathf.PI * frequency / halfWaveWidth) * amplitude;
            Vector3 nextPointOnLine = startPos + transform.right * nextX + transform.up * nextY;

            Debug.DrawLine(pointOnLine, nextPointOnLine, Color.blue); // Draw debug line segment
        }
    }
}
