using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public Text timerText;

    [Header("Timer Settings")]
    private TimeSpan timeOnClock;
    public float currentTime;
    public bool countDown;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
        
        if(hasLimit && ((countDown && currentTime <= timerLimit) ||(!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText();
            timerText.color = Color.red;
            enabled = false;
        }
        timeOnClock = TimeSpan.FromSeconds(currentTime);
        SetTimerText() ;
    }

    private void SetTimerText()
    {
        timerText.text = ("Time: " + timeOnClock.ToString("mm':'ss'.'fff"));
    }
}