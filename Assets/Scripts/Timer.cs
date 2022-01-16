using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // To use TimeSpan

public class Timer : MonoBehaviour
{
    float currentTime;
    public int startMinutes;
    public int cycleNo = 0;
    public Text currentTimeText;
    public Text YearText;
    bool timerActive = false;

    public GameObject controller;
    public GameObject UIparams;

    // Start is called before the first frame update
    void Start()
    {
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(timerActive)
        {
            currentTime -= Time.deltaTime;
            /*
             Decrement the current time with the number of milliseconds
            that passed since the last frame. This ensures that the timer
            shows the correct time irrespective of frame rate.
            */
            if(currentTime<=0)
            {
                StopTimer();
            }
        }
        // Using System to convert float to time format easily
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();

    }

    public void StartTimer()
    {
        currentTime = startMinutes * 5;
        cycleNo++;
        YearText.text = "Year: " + cycleNo.ToString();
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
        controller.GetComponent<EnvironmentAnalyser>().TotalObjectImpact();
        controller.GetComponent<EnvironmentAnalyser>().getQuality();
        UIparams.GetComponent<WorldUI>().UpdateParameters();
        StartTimer();
    }
}
