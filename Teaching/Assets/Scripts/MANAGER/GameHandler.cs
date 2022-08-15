using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    
    public enum GameState {
        Normal,
        GameOver
    }

    public static EventHandler OnGameEnded;

    public static GameHandler instance;

    private GameState gameState = GameState.Normal;

    private void Awake() {
        instance = this;
    }

    private void Update() {
        switch (gameState) {
            case GameState.Normal:

            break;
            case GameState.GameOver:
                GameState_GameOver();
            break;
        }
    }

    private void GameState_GameOver() {
        // Restart
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public static void EndGame() {
        instance.gameState = GameState.GameOver;

        if (OnGameEnded != null) {
            OnGameEnded?.Invoke(instance, EventArgs.Empty);
        }

        // OnGameEnded?.Invoke(instance, EventArgs.Empty);
    }

}
