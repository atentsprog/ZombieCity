using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : GaugeUI<HealthUI>{}
public class GaugeUI<T> : SingletonMonoBehavior<T>
where T : SingletonBase
{
    TextMeshProUGUI valueText;
    public Image[] images; // 8
    public Sprite enable, current, disable;
    protected override void OnInit()
    {
        valueText = transform.Find("ValueText")
            .GetComponent<TextMeshProUGUI>();
    }
    internal void SetGauge(int value, int maxValue) // 50, 100
    {
        valueText.text = $"{value}/{maxValue}";

        int testInt = value / maxValue;
        print(testInt);

        float percent = (float)value / maxValue; // 0.5 * images.Length(8) = 4
        int currentCount = Mathf.RoundToInt(percent * images.Length);
        for (int i = 0; i < images.Length; i++)
        {
            if (i == currentCount)
                images[i].sprite = current;
            else if ( i < currentCount)
                images[i].sprite = enable;
            else
                images[i].sprite = disable;
        }
    }
}
