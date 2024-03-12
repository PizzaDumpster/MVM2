using System.Collections;
using UnityEngine;

public class UpAndDownMovement : MonoBehaviour
{
    public AnimationCurve curve;
    public float moveDistance = 1f; // Distance to move up and down
    public float duration = 1f; // Duration of each movement

    private void Start()
    {
        // Start the coroutine for the movement
        StartCoroutine(MoveUpDown());
    }

    private IEnumerator MoveUpDown()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.up * moveDistance;

        float time = 0f;

        while (true)
        {
            while (time < duration)
            {
                // Calculate the normalized time value based on the animation curve
                float t = curve.Evaluate(time / duration);

                // Interpolate between start and target positions using the animation curve
                transform.position = Vector3.Lerp(startPos, targetPos, t);

                time += Time.unscaledDeltaTime;
                yield return null;
            }

            // Reset time for the next movement
            time = 0f;

            // Swap start and target positions for the next movement
            Vector3 temp = startPos;
            startPos = targetPos;
            targetPos = temp;
        }
    }
}
