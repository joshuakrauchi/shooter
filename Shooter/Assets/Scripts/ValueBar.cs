using UnityEngine;
using UnityEngine.UI;

public class ValueBar : MonoBehaviour
{
    private Slider _slider;

    public void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void SetMaxValue(float value)
    {
        _slider.maxValue = value;
    }

    public void SetValue(float value)
    {
        _slider.value = value;
    }
}