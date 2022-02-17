using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // To use TimeSpan
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    float currentTime;
    public int startSec, currentSec;
    public int cycleNo = 0;
    public Text currentTimeText;
    public Text YearText;
    bool timerActive = false;

    public GameObject controller;
    public GameObject GameOverScreen;
    public GameObject StartGameScreen;
    public GameObject RedCrossScreen;
    public GameObject UIparams;

    public Text highScore;

    // Start is called before the first frame update
    void Start()
    {
        currentSec = startSec;
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
        currentTime = currentSec;
        currentSec+=startSec;
        cycleNo++;
        YearText.text = "Year: " + cycleNo.ToString();
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
        controller.GetComponent<EnvironmentAnalyser>().TotalObjectImpact();
        controller.GetComponent<EnvironmentAnalyser>().getQuality();
        if(UIparams.GetComponent<WorldUI>().checkLevelCompletion())
        {
            UIparams.GetComponent<WorldUI>().UpdateParameters();
            StartTimer();
        }
        else
        {
            GameOverScreen.SetActive(true);
        }
    }

    public void ResetTimer()
    {
        cycleNo = 0;
        currentSec = startSec;
    }

    public void continueButtonFn()
    {
        GameOverScreen.SetActive(false);
        StartGameScreen.SetActive(true);
        highScore.text = "Your highscore is " + cycleNo.ToString() + " Years";
    }

    public void startGameButtonFn()
    {
        StartGameScreen.SetActive(false);
        SceneManager.LoadScene("SampleScene");
    }

    public void showRedCross()
    {
        GameOverScreen.SetActive(false);
        RedCrossScreen.SetActive(true);
    }

    public void goBackFromRedCross()
    {
        GameOverScreen.SetActive(true);
        RedCrossScreen.SetActive(false);
    }
}
