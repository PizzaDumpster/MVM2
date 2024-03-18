using UnityEngine;

public class TimeScaleReporter : MonoBehaviour
{
    // Variable to hold the current time scale
    public float CurrentTimeScale;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure this object persists through scene changes
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the CurrentTimeScale variable every frame
        CurrentTimeScale = Time.timeScale;
    }
}
