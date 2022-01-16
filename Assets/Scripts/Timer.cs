using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // To use TimeSpan

public class Timer : MonoBehaviour
{
    float currentTime;
    public int startMinutes;
    public Text currentTimeText;
    bool timerActive = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMinutes * 60;
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
                timerActive = false;
            }
        }
        // Using System to convert float to time format easily
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();

    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
