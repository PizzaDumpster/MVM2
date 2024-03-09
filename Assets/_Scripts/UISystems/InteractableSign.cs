using System;
using System.Collections;
using UnityEngine;

public class FadeInComplete : BaseMessage { }

public class FadeOutComplete : BaseMessage { }

public class InteractableSign : MonoBehaviour
{

    public ObjectStringSO playerTag;

    [Header("")]
    public SpriteRenderer buttonRenderer;
    public GameObject signTextObject;

    [Header("")]
    public float fadeSpeed = 1f;
    public float maxAlpha = 1f;
    private bool isPlayerInside = false;

    void Start()
    {
        if (buttonRenderer == null)
            buttonRenderer = GetComponent<SpriteRenderer>();

        Color initialColor = buttonRenderer.color;
        initialColor.a = 0f;
        buttonRenderer.color = initialColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag.objectString))
        {
            isPlayerInside = true;
            StartCoroutine(FadeIn(()=> signTextObject.SetActive(true)));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        signTextObject.SetActive(false);
        if (other.CompareTag(playerTag.objectString))
        {
            isPlayerInside = false;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn(Action onComplete = null)
    {
        Color color = buttonRenderer.color;
        while (color.a < maxAlpha)
        {
            color.a += fadeSpeed * Time.deltaTime;
            buttonRenderer.color = color;
            yield return null;
        }

        onComplete?.Invoke();
    }

    IEnumerator FadeOut(Action onComplete = null)
    {
        Color color = buttonRenderer.color;
        while (color.a > 0)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            buttonRenderer.color = color;
            yield return null;
        }

        onComplete?.Invoke();
    }
}
