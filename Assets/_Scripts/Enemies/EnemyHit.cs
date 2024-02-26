using System.Collections;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public int pointsCount;
    public float maxRadius;
    public float speed;
    public float startWidth;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = pointsCount + 1;
        StartCoroutine(Blast());
    }

    private IEnumerator Blast()
    {
        float currentRadius = 0f;
        Transform blastCenter = transform; // Use the position of this object as the center of the blast

        while (currentRadius < maxRadius)
        {
            currentRadius += Time.deltaTime * speed;
            Draw(blastCenter.position, currentRadius);
            yield return null;
        }

        // Disable the object by returning it to the pool
        Destroy(this.gameObject);
    }

    private void Draw(Vector3 blastCenter, float currentRadius)
    {
        float angleBetweenPoints = 360f / pointsCount;

        for (int i = 0; i <= pointsCount; i++)
        {
            float angle = i * angleBetweenPoints * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
            Vector3 position = blastCenter + direction * currentRadius; // Use blast center position

            lineRenderer.SetPosition(i, position);
        }
        lineRenderer.widthMultiplier = Mathf.Lerp(0, startWidth, 1f - currentRadius / maxRadius);
    }
}
