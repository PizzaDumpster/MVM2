using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealthListener : MonoBehaviour
{
    public Slider healthSlider;
    void Start()
    {
        MessageBuffer<EnemyHealthBar>.Subscribe(SetHealth);
        MessageBuffer<BossFightStarted>.Subscribe(HealthBarOn);
        MessageBuffer<BossFightEnded>.Subscribe(HealthBarOff);
        this.gameObject.SetActive(false);
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
        this.gameObject.SetActive(true);
    }


}
