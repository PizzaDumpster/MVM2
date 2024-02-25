using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthListener : MonoBehaviour
{
    public Slider healthSlider;
    void Start()
    {
        MessageBuffer<PlayerCurrentHealth>.Subscribe(SetHealth);
    }

    private void SetHealth(PlayerCurrentHealth obj)
    {
        healthSlider.maxValue = obj.healthData.currentMaxHealth;
        healthSlider.value = obj.healthData.currentHealth;
    }


}
