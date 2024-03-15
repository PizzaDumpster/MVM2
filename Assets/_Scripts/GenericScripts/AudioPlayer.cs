using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance { get; private set; }

    private AudioSource audioSource;
    private float originalPitch; // Store the original pitch value
    private AudioClip currentClip;

    void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Store the original pitch value during initialization
        originalPitch = audioSource.pitch;
    }

    public void PlayAudioClip(AudioClip clip)
    {
        PlayAudioClip(clip, false);
    }
    public void PlayAudioClip(AudioClip clip, bool randomPitch = false)
    {
        if (clip != null)
        {
            if (!audioSource.isPlaying && clip != currentClip)
            {

                currentClip = clip;
                // Set the audio clip
                audioSource.clip = clip;

                if (randomPitch)
                {
                    // Generate a random pitch value within a specified range
                    float minPitch = 0.9f; // Adjust as needed
                    float maxPitch = 1.1f; // Adjust as needed
                    float randomPitchValue = Random.Range(minPitch, maxPitch);

                    // Apply the random pitch to the audio source
                    audioSource.pitch = randomPitchValue;
                }
                else
                {
                    // Use the original pitch if randomPitch is set to false
                    audioSource.pitch = originalPitch;
                }

                // Play the audio
                audioSource.Play();
            }
        }
    }

    private void Update()
    {
        // Check if the audio clip has finished playing
        if (currentClip != null && !audioSource.isPlaying)
        {
            // Reset the current clip so it can be played again
            currentClip = null;
        }
    }

    public void PitchAdjust(float pitch)
    {
        audioSource.pitch = pitch;
    }


    // Additional methods to control audio can be added here
    // e.g., StopAudio, PauseAudio, ChangeVolume, etc.
}
