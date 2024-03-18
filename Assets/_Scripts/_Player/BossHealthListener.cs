using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BossHealthListener : MonoBehaviour
{
    public Slider healthSlider;
    public TextMeshProUGUI bossName;
    void Start()
    {
        MessageBuffer<EnemyHealthBar>.Subscribe(SetHealth);
        MessageBuffer<BossFightStarted>.Subscribe(HealthBarOn);
        MessageBuffer<BossFightEnded>.Subscribe(HealthBarOff);
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        MessageBuffer<EnemyHealthBar>.Unsubscribe(SetHealth);
        MessageBuffer<BossFightStarted>.Unsubscribe(HealthBarOn);
        MessageBuffer<BossFightEnded>.Unsubscribe(HealthBarOff);
    }

    private void SetHealth(EnemyHealthBar obj)
    {
        healthSlider.maxValue = obj.healthData.currentMaxHealth;
        healthSlider.value = obj.healthData.currentHealth;
    }

    private void HealthBarOff(BossFightEnded obj)
    {
        this.gameObject.SetActive(false);
    }

    private void HealthBarOn(BossFightStarted obj)
    {
        bossName.text = obj.BossName;
        this.gameObject.SetActive(true);
    }


}
