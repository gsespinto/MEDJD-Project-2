using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int targetScore = 5;
    string targetScoreText;
    int currentScore = 0;
    [SerializeField] int nextScene = 0;

    void Start()
    {
        // Sets initial visual values
        ChangeScore(0);
    }

    /// <summary> Change score by given amount, if it has reached target score, load next scene </summary>
    public void ChangeScore(int amount)
    {
        // Change score by given amount
        currentScore += amount;
        // Update visuals
        scoreText.text = "x " + currentScore + " / " + targetScore;

        // If score has reached its target value
        // Load next scene
        if (currentScore >= targetScore)
            GameObject.FindObjectOfType<SceneLoader>().LoadScene(nextScene);
    }
}
