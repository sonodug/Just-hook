using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Slider Slider;

    public virtual void OnValueChanged(float value, float maxValue)
    {
        Slider.value = value / maxValue;
    }

    public float GetSliderValue()
    {
        return Slider.value;
    }
}
