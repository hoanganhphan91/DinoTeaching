using System;
using UnityEngine;

public class SpikeObject : MonoBehaviour
{

    private enum State {
        Normal,
        Pause
    }
    
    [SerializeField] private float moveSpeed;

    [SerializeField] private float lifeTime = 20;

    private State state = State.Normal;

    private void Awake() {
        GameHandler.OnGameEnded += GameHandler_OnGameEnded;
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
                MoveHandler();
                SelfDestroyHandler();
            break;
            case State.Pause:

            break;
        }
    }

    private void MoveHandler() {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    private void SelfDestroyHandler() {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Player player = collider.GetComponent<Player>(); // Lay Player to other
        if (player != null) {
            player.KillPlayer();
        } 
    }

}
