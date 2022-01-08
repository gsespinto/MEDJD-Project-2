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
        ChangeScore(0);
    }

    public void ChangeScore(int amount)
    {
        currentScore += amount;
        scoreText.text = "x " + currentScore + " / " + targetScore;

        if (currentScore >= targetScore)
            GameObject.FindObjectOfType<SceneLoader>().LoadScene(nextScene);
    }
}
