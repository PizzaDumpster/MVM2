using UnityEngine;

public class PingPongWavePath : MonoBehaviour
{
    public Transform startPoint;
    public float frequency = 1f; // Adjust this to change the frequency of the wave
    public float amplitude = 1f; // Adjust this to change the amplitude of the wave
    public float speed = 1f; // Adjust this to change the speed of the object
    public bool useCosine = false; // Use cosine wave instead of sine
    public float halfWaveWidth = 1f; // Adjust this to change the width of the wave path

    public int numberOfPoints = 100; // Adjust this to change the resolution of the debug path
    public Color debugPathColor = Color.red;

    private float time = 0f;
    private Vector3 startPos;
    private bool movingForward = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Calculate the position along the wave path
        float waveOffset = useCosine ? Mathf.Cos(time * speed * Mathf.PI * frequency) : Mathf.Sin(time * speed * Mathf.PI * frequency);
        float xPos = useCosine ? Mathf.Cos(time * speed * Mathf.PI * frequency) : time;
        float yPos = waveOffset * amplitude;
        Vector3 offset = new Vector3(xPos * halfWaveWidth, yPos, 0f);

        // Update the position of the object
        transform.position = startPos + offset;

        // Increment time
        time += Time.deltaTime * (movingForward ? 1f : -1f);

        // Check if the object reaches the end of the wave path and reverse the direction
        if (time >= 1f / (speed * frequency))
        {
            time = 1f / (speed * frequency);
            movingForward = false;
        }
        else if (time <= 0f)
        {
            time = 0f;
            movingForward = true;
        }

        // Debug draw the wave path
        DebugDrawWavePath();
    }

    void DebugDrawWavePath()
    {
        Vector3 startPos = startPoint.position;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i / (float)(numberOfPoints - 1);
            float x = Mathf.Lerp(-1f, 1f, t);
            float y = useCosine ? Mathf.Cos(x * Mathf.PI * frequency) * amplitude : Mathf.Sin(x * Mathf.PI * frequency) * amplitude;
            Vector3 pointOnLine = startPos + new Vector3(x * halfWaveWidth, y, 0f);

            // Calculate the next point on the wave
            float nextT = (i + 1) / (float)(numberOfPoints - 1);
            float nextX = Mathf.Lerp(-1f, 1f, nextT);
            float nextY = useCosine ? Mathf.Cos(nextX * Mathf.PI * frequency) * amplitude : Mathf.Sin(nextX * Mathf.PI * frequency) * amplitude;
            Vector3 nextPointOnLine = startPos + new Vector3(nextX * halfWaveWidth, nextY, 0f);

            Debug.DrawLine(pointOnLine, nextPointOnLine, debugPathColor);
        }
    }
}
