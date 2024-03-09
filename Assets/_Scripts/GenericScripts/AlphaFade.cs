using System.Collections;
using UnityEngine;

public class AlphaFade : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float fadeSpeed = 1f;
    public float maxAlpha = 1f;
    private bool isPlayerInside = false;

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        // Set initial alpha to 0
        Color initialColor = spriteRenderer.color;
        initialColor.a = 0f;
        spriteRenderer.color = initialColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            StartCoroutine(FadeIn());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        Color color = spriteRenderer.color;
        while (color.a < maxAlpha)
        {
            color.a += fadeSpeed * Time.deltaTime;
            spriteRenderer.color = color;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        Color color = spriteRenderer.color;
        while (color.a > 0)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            spriteRenderer.color = color;
            yield return null;
        }
    }
}
