using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private const string HIGH_SCORE = "HighScore";

    private enum State {
        Counting,
        Stopped
    }

    public static EventHandler<OnScoreChangeEventArgs> OnScoreChange;
    public class OnScoreChangeEventArgs : EventArgs {
        public int score;
    }

    public static EventHandler<OnHighScoreChangeEventArgs> OnHighScoreChange;
    public class OnHighScoreChangeEventArgs : EventArgs {
        public int highScore;
    }

    public static ScoreManager instance;
    
    private float runTime;
    private int score;
    private int highScore;

    private State state = State.Counting;

    private void Awake() {
        instance = this;

        score = 0;
        highScore = PlayerPrefs.GetInt(HIGH_SCORE, 0);

        GameHandler.OnGameEnded += GameHandler_OnGameEnded;
    }

    private void OnDestroy() {
        GameHandler.OnGameEnded -= GameHandler_OnGameEnded;
    }

    private void GameHandler_OnGameEnded(object sender, EventArgs e) {
        state = State.Stopped;

        TryUpdateHighScore();
    }

    private void Update() {
        switch (state) {
            case State.Counting:
                CountingHandler();
            break;
            case State.Stopped:
                
            break;
        }

        ResetHighScoreHandler();
    }

    private void CountingHandler() {
        runTime += Time.deltaTime;

        score = Mathf.RoundToInt(runTime);
        OnScoreChange?.Invoke(this, new OnScoreChangeEventArgs {
            score = score
        });
    }

    private void TryUpdateHighScore() {
        if (score > highScore) {
            highScore = score;

            OnHighScoreChange?.Invoke(this, new OnHighScoreChangeEventArgs {
                highScore = highScore
            });

            PlayerPrefs.SetInt(HIGH_SCORE, highScore);
        }
    }

    private void ResetHighScoreHandler() {
        if (Input.GetKeyDown(KeyCode.K)) {
            PlayerPrefs.SetInt(HIGH_SCORE, 0);
            highScore = 0;
            OnHighScoreChange?.Invoke(this, new OnHighScoreChangeEventArgs {
                highScore = highScore
            });
        }
    }

    public static int GetHighScore() {
        return instance.highScore;
    }

}
