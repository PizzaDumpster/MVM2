using System.Collections;
using UnityEngine;

public class PlayerDashController : MonoBehaviour
{
    private DashMeter dashMeter = new DashMeter();
    private float currentDashCooldown = 100f;

    public bool CanDash => currentDashCooldown >= 95;

    public void StartDashCooldown(float cooldownTime)
    {
        currentDashCooldown = dashMeter.DashAmount;
        StartCoroutine(DashCooldownRoutine(cooldownTime));
    }

    private IEnumerator DashCooldownRoutine(float cooldownTime)
    {
        float elapsedTime = 0f;

        while (elapsedTime < cooldownTime)
        {
            float timePercentage = elapsedTime / cooldownTime;

            dashMeter.DashAmount = Mathf.Lerp(0f, 100f, timePercentage);
            currentDashCooldown = dashMeter.DashAmount;
            MessageBuffer<DashMeter>.Dispatch(dashMeter);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        currentDashCooldown = 100f;
        MessageBuffer<DashMeter>.Dispatch(dashMeter);

    }
}
