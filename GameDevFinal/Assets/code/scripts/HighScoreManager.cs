using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public Text highScoreText;

    private void Start()
    {
        DisplayHighScore();
    }

    private void DisplayHighScore()
    {
        // Retrieve the current wave and subtract 1 to get the desired behavior
        int currentWave = PlayerPrefs.GetInt("CurrentWave", 1) - 1;

        // Ensure the high score is never negative
        int highScore = Mathf.Max(PlayerPrefs.GetInt("HighScore", 0), 0);

        if (currentWave > highScore)
        {
            highScore = currentWave;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        highScoreText.text = "High Score: " + highScore;
    }
}
