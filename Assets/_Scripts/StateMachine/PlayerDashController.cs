using System.Collections;
using UnityEngine;

public class PlayerDashController : MonoBehaviour
{
    private DashMeter dashMeter = new DashMeter();
    private float currentDashCooldown = 0.0f;

    public bool CanDash => currentDashCooldown == 0;

    public void StartDashCooldown(float cooldownTime)
    {
        StartCoroutine(DashCooldownRoutine(cooldownTime));
    }

    private IEnumerator DashCooldownRoutine(float cooldownTime)
    {
        float elapsedTime = 0f;

        while (elapsedTime < cooldownTime)
        {
            float timePercentage = elapsedTime / cooldownTime;

            dashMeter.DashAmount = Mathf.Lerp(0f, 100f, timePercentage);

            MessageBuffer<DashMeter>.Dispatch(dashMeter);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        currentDashCooldown = 100f;
        MessageBuffer<DashMeter>.Dispatch(dashMeter);
        currentDashCooldown = 0f;
    }
}
