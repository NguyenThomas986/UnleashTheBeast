
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 4f;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movement;
    private Vector2 lastDirection = Vector2.down;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontal, vertical).normalized;

        bool isMoving = movement.sqrMagnitude > 0.01f;

        if (isMoving)
        {
            // Determine which direction the character faces.
            if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
            {
                lastDirection = horizontal > 0
                    ? Vector2.right
                    : Vector2.left;
            }
            else if (Mathf.Abs(vertical) > 0f)
            {
                lastDirection = vertical > 0
                    ? Vector2.up
                    : Vector2.down;
            }
        }

        animator.SetBool("IsMoving", isMoving);

        animator.SetFloat("MoveX", lastDirection.x);
        animator.SetFloat("MoveY", lastDirection.y);

        animator.SetFloat("LastMoveX", lastDirection.x);
        animator.SetFloat("LastMoveY", lastDirection.y);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }
}
