using System;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{

    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI highScoreText;
    
    private void Awake() {
        scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        highScoreText = transform.Find("HighScoreText").GetComponent<TextMeshProUGUI>();

        highScoreText.text = "HighScore: " + ScoreManager.GetHighScore().ToString();

        ScoreManager.OnScoreChange += ScoreManager_OnScoreChange;
        ScoreManager.OnHighScoreChange += ScoreManager_OnHighScoreChange;
    }

    private void OnDestroy() {
        ScoreManager.OnScoreChange -= ScoreManager_OnScoreChange;
        ScoreManager.OnHighScoreChange -= ScoreManager_OnHighScoreChange;
    }

    private void ScoreManager_OnScoreChange(object sender, ScoreManager.OnScoreChangeEventArgs e) {
        scoreText.text = ("Score: " + e.score.ToString());
    }

    private void ScoreManager_OnHighScoreChange(object sender, ScoreManager.OnHighScoreChangeEventArgs e) {
        highScoreText.text = ("HighScore: " + e.highScore.ToString());
    }

}
