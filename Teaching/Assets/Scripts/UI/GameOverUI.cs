using System;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{

    private TextMeshProUGUI gameOverText;
    
    private void Awake() {
        gameOverText = GetComponent<TextMeshProUGUI>();

        GameHandler.OnGameEnded += GameHandler_OnGameEnded;
    }

    private void OnDestroy() {
        GameHandler.OnGameEnded -= GameHandler_OnGameEnded;
    }

    private void GameHandler_OnGameEnded(object sender, EventArgs e) {
        gameOverText.enabled = true;
    }

}
