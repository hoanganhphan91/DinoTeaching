using UnityEngine;

public class Player : MonoBehaviour
{

    public enum State {
        Normal,
        Dead
    }
    
    private Rigidbody2D rb2D;
    [SerializeField] private float jumpSpeed;

    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Vector2 groundCheckPositionOffset;
    [SerializeField] private Vector2 groundCheckSize;

    private State state = State.Normal;

    private void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        switch (state) {
            case State.Normal:
                JumpControlHandler();
            break;
            case State.Dead:

            break;
        }
    }

    private void JumpControlHandler() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            TryJump();
        }
    }

    private void TryJump() {
        if (!IsGrounded()) {
            return;
        }

        rb2D.velocity = new Vector2(0f, jumpSpeed);
    }

    private bool IsGrounded() {
        Vector2 checkPosition = (Vector2)transform.position + groundCheckPositionOffset;
        Collider2D[] colliderArray = Physics2D.OverlapBoxAll(checkPosition, groundCheckSize, 0f, groundLayerMask);
        return (colliderArray.Length > 0);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube((Vector2)transform.position + groundCheckPositionOffset, groundCheckSize);    
    }

    public void KillPlayer() {
        state = State.Dead; // Khong the nhay
        rb2D.simulated = false; // Khong roi, va cham
        
        GameHandler.EndGame();
    }

}
