using UnityEngine;

public class FollowWavePath : MonoBehaviour
{
    public Transform startPoint;
    public float frequency = 1f; // Adjust this to change the frequency of the wave
    public float amplitude = 1f; // Adjust this to change the amplitude of the wave
    public float speed = 1f; // Adjust this to change the speed of the object
    public bool useCosine = false; // Use cosine wave instead of sine

    private float time = 0f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Calculate the position along the wave path
        float waveOffset = useCosine ? Mathf.Cos(time * speed * Mathf.PI * frequency) : Mathf.Sin(time * speed * Mathf.PI * frequency);
        Vector3 offset = transform.right * waveOffset * amplitude;

        // Update the position of the object
        transform.position = startPos + offset;

        // Increment time
        time += Time.deltaTime;

        // If you want the object to move back and forth, you can reset the time when it reaches the end of the wave path
        if (time >= 1f / (speed * frequency))
        {
            time = 0f;
            startPos = transform.position;
        }
    }
}
