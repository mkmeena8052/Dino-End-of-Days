using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [SerializeField] private Transform groundCheckTransform;
    public LayerMask groundLayer;
    public float groundCheckRadius;
    public bool isGrounded;
    float move;
    Rigidbody2D rb;

    void Awake()
    {
        playerInput = new PlayerInput();
    }

    void OnEnable()
    {
        playerInput.Player.Enable();
        playerInput.Player.Move.performed += Movement;
        playerInput.Player.Move.canceled += Movement;
        playerInput.Player.Jump.performed += Jumping;
    }

    void OnDisable()
    {
        playerInput.Player.Disable();
        playerInput.Player.Move.performed -= Movement;
        playerInput.Player.Move.canceled -= Movement;
        playerInput.Player.Jump.performed -= Jumping;
    }



    void Movement(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>().x;
    }

    void Jumping(InputAction.CallbackContext context) {
        if (context.performed) {
            if (isGrounded) {
                rb.linearVelocityY = jumpForce;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
    }

    public void gameOver()
    {
        // Kills player
        Debug.Log("Game Over");
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayer);
        rb.linearVelocityX = move * speed;
    }
}
