using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class DayLightController : MonoBehaviour
{
    const float SecondsInDay = 86400f;

    [SerializeField] Color nightLightColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayLightColor = Color.white;

    [SerializeField] float timeScale;
    [SerializeField] Text Time_TXT;
    [SerializeField] Light2D globalLight;
    private int days;

    float time;

    float Hours
    {
        get { return time / 3600f; }
    }

    float Minutes
    {
        get { return time % 3600f / 60f; }
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;
        int hh = (int)Hours;
        int mm = (int)Minutes;
        Time_TXT.text = hh.ToString("00") + ":" + mm.ToString("00");
        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;
        if(time > SecondsInDay)
        {
            NextDay();
        }
    }

    private void NextDay()
    {
        time = 0;
        days += 1;
    }
}
