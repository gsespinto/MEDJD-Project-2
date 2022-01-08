using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    int currentScore = 0;
    [SerializeField] FlightMovement flightMovement;

    void Start()
    {
        ChangeScore(0);
    }

    public void ChangeScore(int amount)
    {
        currentScore += amount;
        if (currentScore < 10)
            scoreText.text = "x 0" + currentScore;
        else
            scoreText.text = "x " + currentScore;
    }
}
