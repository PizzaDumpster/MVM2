using UnityEngine;
using DG.Tweening;
using System;

public class PillarShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeStrength = 0.2f;
    public float moveDownDistance = 1.0f;
    public float moveDownDuration = 0.5f;

    private bool playerLanded = false;
    private Vector3 originalPosition;

    private void Start()
    {
        MessageBuffer<PlayerRespawn>.Subscribe(ResetPosition);
        originalPosition = transform.position;
    }

    public void OnDestroy()
    {
        MessageBuffer<PlayerRespawn>.Unsubscribe(ResetPosition);
    }

    private void ResetPosition(PlayerRespawn obj)
    {
        transform.DOKill();

        transform.position = originalPosition;
        playerLanded = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);

        if (other.collider.CompareTag("Player") && !playerLanded)
        {
            Debug.Log("Player landed on the pillar.");

            playerLanded = true;

            // Shake animation
            transform.DOShakePosition(shakeDuration, shakeStrength)
                .OnComplete(() =>
                {
                    // Move down animation
                    transform.DOMoveY(originalPosition.y - moveDownDistance, moveDownDuration)
                        .SetEase(Ease.OutQuad);
                });
        }
    }
}
