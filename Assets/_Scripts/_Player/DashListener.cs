using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashListener : MonoBehaviour
{
    public PowerUpSO requiredPower;
    public GameObject sliderObject;
    public Slider dashSlider;

    private void Awake()
    {
        MessageBuffer<DashMeter>.Subscribe(SetDash);
        MessageBuffer<PickedUpPowerUp>.Subscribe(SetDashActive);
        if (sliderObject) sliderObject.SetActive(false);
    }

    private void SetDashActive(PickedUpPowerUp obj)
    {
        if (obj.powerUp == requiredPower) { sliderObject.SetActive(true); } else return;
    }

    private void SetDash(DashMeter obj)
    {
        dashSlider.maxValue = 100;
        dashSlider.value = obj.DashAmount;
    }
}
