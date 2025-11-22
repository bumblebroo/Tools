using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDown2DController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    float speed = 3;

    [SerializeField]
    [Tooltip("Time it takes to go from zero to the desired speed")]
    float accelerationTime = 0.3f;

    [SerializeField]
    private bool canMove = true;

    private Vector2 velocity;
    private Vector2 dir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        velocity = Vector2.MoveTowards(velocity, dir * speed, Time.fixedDeltaTime * (speed / accelerationTime));

        if (!canMove) {
            velocity = Vector2.zero;
        }

        rb.linearVelocity = velocity;
    }

    public void OnMove(InputAction.CallbackContext context) {
        dir = context.ReadValue<Vector2>();
    }

    public void SetCanMove(bool value) {
        canMove = value;
    }
}
