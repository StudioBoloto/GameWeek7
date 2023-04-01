using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 60;
    public TMP_Text timerText;
    public bool isTimerRunning = false;

    void Start()
    {
        isTimerRunning = true;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; 
                DisplayTime(timeRemaining);
            }
            else
            {
                isTimerRunning = false;
                DisplayTime(timeRemaining);
                GameOver();
            }
        }
    }
    

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        
        if (timeToDisplay < 6f)
        {
            timerText.color = Color.red;
            //timerText.GetComponent<Animator>().enabled = true;
        }
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene"); 
    }

    public void GameWin()
    {
        SceneManager.LoadScene("GameWin"); 
    }

    public void SubtractTime(float penaltyTime)
    {
        timeRemaining -= penaltyTime;
    }
}
