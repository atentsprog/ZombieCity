using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : GaugeUI<HealthUI>
{
}

public class GaugeUI<T> : SingletonMonoBehavior<T>
where T : SingletonMonoBehavior<T>
{
    protected TextMeshProUGUI countText;
    public Image[] images;
    public Sprite active, current, notActive;
    int ImageCount => images.Length;

    protected override void OnInit()
    {
        countText = transform.Find("CountText").GetComponent<TextMeshProUGUI>();
    }

    internal void SetGuage(int current, int max)
    {
        //print(current);
        countText.text = $"{current}/{max}";

        float fPercent = (float)current / max; // 1
        int percentCount = Mathf.RoundToInt(fPercent * ImageCount)-1; // 8
        for (int i = 0; i < ImageCount; i++)
        {
            if (percentCount == i)
                images[i].sprite = this.current;
            else if (percentCount < i)
                images[i].sprite = notActive;
            else
                images[i].sprite = active;
        }
    }

    internal void Restore(float duration)
    {
        StartCoroutine(RestoreCo( duration));
    }

    private IEnumerator RestoreCo(float duration)
    {
        foreach (var item in images)
            item.sprite = notActive;

        float timePerEach = duration / ImageCount;
        for (int i = 0; i < ImageCount; i++)
        {
            if(i == ImageCount - 1)
                images[i].sprite = current;
            else
                images[i].sprite = active;

            yield return new WaitForSeconds(timePerEach);
        }
    }
}