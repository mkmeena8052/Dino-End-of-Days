using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;
    Rigidbody2D rigidbody;

    [SerializeField] private float METEORSPEED = 0.5f;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start() {
        rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, -METEORSPEED);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovement.gameOver();
        } else if (collision.gameObject.CompareTag("Ground"))
        {
            // Should shatter into pieces.
        }
    }
}
