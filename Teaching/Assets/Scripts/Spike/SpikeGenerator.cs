using System;
using UnityEngine;

public class SpikeGenerator : MonoBehaviour
{

    private enum State {
        Normal,
        Pause
    }
   
    [SerializeField] private float spawnTimerMax;
    private float spawnTimer = 0f;

    [SerializeField] private Transform spikePf;
    [SerializeField] private Transform spawnPointTransform;

    private State state = State.Normal;

    private void Awake() {
        GameHandler.OnGameEnded += GameHandler_OnGameEnded;     // Nghe duoc rang game da dung
    }

    private void OnDestroy() {
        GameHandler.OnGameEnded -= GameHandler_OnGameEnded;
    }

    private void GameHandler_OnGameEnded(object sender, EventArgs e) {
        state = State.Pause;
    }

    private void Update() {
        switch (state) {
            case State.Normal:
                SpawnSpikeHandler();
            break;
            case State.Pause:

            break;
        }
    }

    private void SpawnSpikeHandler() {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0) {    
            spawnTimer = spawnTimerMax;

            CreateSpike();
        }
    }

    private void CreateSpike() {
        Instantiate(spikePf, spawnPointTransform.position, Quaternion.identity);
    }

}
