using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    int currentScore = 0;
    [SerializeField] float timer = 0;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] FlightMovement flightMovement;

    void Start()
    {
        ChangeScore(0);
    }

    void Update()
    {
        TickTimer();
    }

    public void ChangeScore(int amount)
    {
        currentScore += amount;
        if (currentScore < 10)
            scoreText.text = "x 0" + currentScore;
        else
            scoreText.text = "x " + currentScore;
    }

    public void TickTimer()
    {
        if (GameHasEnded())
        {
            if (flightMovement && flightMovement.enabled)
            {
                flightMovement.enabled = false;
            }

            return;
        }
        
        timer -= Time.deltaTime;
        if ((int)timer < 10)
            timerText.text = "0" + (int)timer;
        else
            timerText.text = ((int)timer).ToString();
    }

    private bool GameHasEnded()
    {
        return timer <= 0.0f;
    }
}
