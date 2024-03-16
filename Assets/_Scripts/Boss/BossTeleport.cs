using UnityEngine;
using DG.Tweening; // Import DOTween namespace
using System.Collections;
using UnityEngine.Events;

public class BossTeleport : MonoBehaviour
{
    public Transform[] teleportLocations; // Array of locations to teleport to
    public float fadeDuration = 1f; // Duration of fade effect
    public float teleportDelay = 2f; // Delay between teleports
    public float fadeOutDelayMin = 1f; // Minimum delay before fading out
    public float fadeOutDelayMax = 3f; // Maximum delay before fading out

    public SpriteRenderer spriteRenderer;
    public CircleCollider2D triggerCollider; // Circle collider for triggering the fade

    private int currentLocationIndex = 0;
    private bool isTeleporting = false;
    public bool isFading = false; // New boolean for indicating if the boss is currently fading

    public UnityEvent onFadeOut;
    public UnityEvent onFadeIn;
    public UnityEvent attack;

    void Start()
    {
        triggerCollider.isTrigger = true;
        triggerCollider.enabled = true;
        this.gameObject.SetActive(false);
    }

    private float timeSinceLastAttack = 0f;
    public float attackInterval = 5f; // Adjust this value according to your desired attack interval

    private void Update()
    {
        if (!isFading)
        {
            timeSinceLastAttack += Time.deltaTime;
            if (timeSinceLastAttack >= attackInterval)
            {
                attack?.Invoke();
                timeSinceLastAttack = 0f; // Reset the counter after invoking the attack
            }
        }
    }

    public void PlayerEntersTrigger()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutProcess());
    }

    IEnumerator FadeOutProcess()
    {
        // Random delay before starting the fade out
        float delay = Random.Range(fadeOutDelayMin, fadeOutDelayMax);
        yield return new WaitForSeconds(delay);

        // Fade out
        FadeOut();
    }

    void FadeOut()
    {
        isFading = true; // Boss is now fading
        spriteRenderer.DOFade(0f, fadeDuration).OnComplete(() => {
            onFadeOut?.Invoke();
            Teleport();
            onFadeIn?.Invoke();
        });
    }

    void Teleport()
    {
        // Teleport to a random location
        currentLocationIndex = Random.Range(0, teleportLocations.Length);
        Debug.Log("Teleporting to location index: " + currentLocationIndex); // Add this debug log
        transform.position = teleportLocations[currentLocationIndex].position;

        // Fade in
        FadeIn();
    }

    void FadeIn()
    {
        spriteRenderer.DOFade(1f, fadeDuration / 2f).OnComplete(() => {
            // Set isTeleporting to false after fade in is complete
            isTeleporting = false;
            isFading = false; // Boss is no longer fading
        });
    }
}
