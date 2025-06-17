using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimerCounter : MonoBehaviour
{
    public Text timerText;
    float timer = 0f;
    bool startTime;

    public void StartTimeCounter()
    {
        startTime = true;
        timer = 0f;
    }

    public void StopTimeCounter()
    {
        startTime = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {

        if (startTime)
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60f);
            int milliseconds = Mathf.FloorToInt((timer * 1000f) % 1000f);

            timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds); 
        }

    }
}
