using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Color nightColor;
    public Color dayColor;

    public float changeDuration = 3;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
            ChangeDayLightCo();

        if (Input.GetKeyDown(KeyCode.Alpha6))
            ChangeNightLightCo();

    }

    private void ChangeNightLightCo()
    {
        DOTween.To(() => nightColor, (x) => RenderSettings.ambientLight = x, nightColor, changeDuration);

        if (allLight == null)
            allLight = new List<Light>(FindObjectsOfType<Light>(true))
                .ToDictionary(x => x, y => y.intensity);

        foreach (var item in allLight.Keys)
        {
            item.enabled = true;
            if (item.type == LightType.Directional)
                DOTween.To(() => item.intensity, (x) => item.intensity = x, 0, changeDuration);
            else
            {
                item.intensity = 0;
                DOTween.To(() => item.intensity, (x) => item.intensity = x, allLight[item], changeDuration);
            }
        }
    }

    Dictionary<Light, float> allLight;
    private void ChangeDayLightCo()
    {
        DOTween.To(() => nightColor, (x) => RenderSettings.ambientLight = x, dayColor, changeDuration);

        if (allLight == null)
            allLight = new List<Light>(FindObjectsOfType<Light>(true))
                .ToDictionary(x => x, y => y.intensity);

        foreach (var item in allLight.Keys)
        {
            item.enabled = true;
            if (item.type == LightType.Directional)
            {
                item.intensity = 0;
                DOTween.To(() => item.intensity, (x) => item.intensity = x, allLight[item], changeDuration);
            }
            else
                DOTween.To(() => item.intensity, (x) => item.intensity = x, 0, changeDuration);
        }
    }

    [ContextMenu("SetNightTime")]
    void SetNightTime()
    {
        // 메인 라인트 점차 끄기.
        // 기타 라이트 점차 켜기.
        // 환경색
        RenderSettings.ambientLight = nightColor;
        var lights = FindObjectsOfType<Light>(true);
        foreach (var item in lights)
        {
            if (item.type == LightType.Directional)
                item.enabled = false;
            else
                item.enabled = true;
        }
    }

    [ContextMenu("SetDayTime")]
    void SetDayTime()
    {
        RenderSettings.ambientLight = dayColor;
        var lights = FindObjectsOfType<Light>(true);
        foreach (var item in lights)
        {
            if (item.type == LightType.Directional)
                item.enabled = true;
            else
                item.enabled = false;
        }
    }
}
