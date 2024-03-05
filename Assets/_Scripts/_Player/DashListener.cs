using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashListener : MonoBehaviour
{
    public Slider dashSlider;
    void Start()
    {
        MessageBuffer<DashMeter>.Subscribe(SetDash);
    }

    private void SetDash(DashMeter obj)
    {
        dashSlider.maxValue = 100;
        dashSlider.value = obj.DashAmount;
    }
}
