using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManagerScript : MonoBehaviour
{
    public static ScoreManagerScript Instance;

    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text newHighScoreText;

    public int score;
    public int highScore;
    public int pointsPerSecond = 1;
    public int pointsPerKill = 100;

    private bool newHighScoreAchieved = false;
    public bool gameRunning = true;

    void Awake()
    {
        if (Instance == null)
        {    
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }    
        else
        {    
            Destroy(gameObject);
            return;
        }

        newHighScoreText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!gameRunning) return;

        score += Mathf.RoundToInt(pointsPerSecond * Time.deltaTime);

        if (score > highScore)
        {
            highScore = score;

            if (!newHighScoreAchieved)
            {
                newHighScoreAchieved = true;
                newHighScoreText.gameObject.SetActive(true);
            }
        }

        UpdateUI();
    }

    public void AddKillScore()
    {
        score += pointsPerKill;
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;

        if (newHighScoreAchieved)
            newHighScoreText.text = "NEW HIGH SCORE: " + highScore;
    }
}
