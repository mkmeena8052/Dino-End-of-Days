using UnityEngine;
using UnityEngine.InputSystem;

class PlayerMovement : MonoBehaviour {
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private HealthHUD healthHUD;

    private Animator anim;

    public LayerMask groundLayer;
    public float groundCheckRadius;
    public bool isGrounded;
    public bool isDead = false;
    public bool canDoubleJump = false;
    public bool canShoot = false;
    private float move;
    private float powerupTimer = 10f;
    private int playerHealth = 3;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    public bool shootPower = false;
    public bool doubleJumpPower = false;

    [SerializeField] private Sprite[] copterSprites;
    [SerializeField] private Sprite[] basicSprites;


    void Awake()
    {
        playerInput = new PlayerInput();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        playerInput.Player.Enable();
        playerInput.Player.Move.performed += Movement;
        playerInput.Player.Move.canceled += Movement;
        playerInput.Player.Jump.performed += Jumping;
        playerInput.Player.Attack.performed += Attacking;
    }

    void OnDisable()
    {
        playerInput.Player.Disable();
        playerInput.Player.Move.performed -= Movement;
        playerInput.Player.Move.canceled -= Movement;
        playerInput.Player.Jump.performed -= Jumping;
        playerInput.Player.Attack.performed -= Attacking;
    }

    void Movement(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>().x;
    }

    void Jumping(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded || (!isGrounded && canDoubleJump))
            {
                FindAnyObjectByType<AudioManager>().Play("Jump");
                rb.linearVelocityY = jumpForce;
                canDoubleJump = false;
            }
        }
    }

    void Attacking(InputAction.CallbackContext context)
    {

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
    }

    public void takeDamage(int amount)
    {
        playerHealth -= amount;
        Debug.Log(playerHealth);
        if (playerHealth <= 0) {
            playerHealth = 0;
            if (!isDead)
            {
                isDead = true;
                playerDied();
            }
        }
        healthHUD.UpdateHealth(playerHealth);
        if (playerHealth > 0) FindAnyObjectByType<AudioManager>().Play("Player Hurt");
    }

    public void playerDied()
    {
        Debug.Log("Player Died!");
        FindAnyObjectByType<AudioManager>().Play("Game Over");
        FindAnyObjectByType<retryButton>().reloadScene();
    }


    void Start() { rb = GetComponent<Rigidbody2D>(); }

    void Update()
    {

        if (doubleJumpPower || shootPower)
        {
            powerupTimer -= Time.deltaTime;
            if (spriteRenderer.sprite.name != "dino_6" || spriteRenderer.sprite.name != "dino_7" || spriteRenderer.sprite.name != "dino_8")
            {

            }
        } else
        {
            if (spriteRenderer.sprite.name != "dino_6" || spriteRenderer.sprite.name != "dino_7" || spriteRenderer.sprite.name != "dino_8")
            {

            }
        }

        if (powerupTimer <= 0)
        {
            powerupTimer = 10f;
            doubleJumpPower = false;
            shootPower = false;
        }
        if (doubleJumpPower && isGrounded) canDoubleJump = true;

        anim.SetFloat("move", move);
        anim.SetBool("doubleJumpPower", doubleJumpPower);
        if (move < 0) spriteRenderer.flipX = true;
        else if (move > 0) spriteRenderer.flipX = false;

        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayer);
        rb.linearVelocityX = move * speed;
    }
}