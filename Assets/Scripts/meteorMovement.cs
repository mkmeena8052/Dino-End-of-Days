using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;
    new Rigidbody2D rigidbody;

    [SerializeField] private float METEORSPEED = 3f;

    [Header("Shatter Settings")]
    [SerializeField] private GameObject boulderPrefab;
    [SerializeField] private int boulderCount = 2;
    [SerializeField] private float boulderSpawnOffset = 0.3f;
    [SerializeField] private float boulderLaunchForce = 2f;
    [SerializeField] private float boulderSize = 0.8f;

    [SerializeField] private Sprite[] sprites;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        rigidbody = GetComponent<Rigidbody2D>();

        transform.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }

    void Update()
    {
        rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, -METEORSPEED);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovement.takeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            ShatterIntoBoulders();
            Destroy(gameObject);
        }
    }

    private void ShatterIntoBoulders()
    {
        for (int i = 0; i < boulderCount; i++)
        {
            // Alternate spawn offset left/right of the meteor's position
            float direction = (i % 2 == 0) ? -1f : 1f;
            Vector3 spawnPos = transform.position + new Vector3(direction * boulderSpawnOffset, 0f, 0f);

            GameObject boulder = Instantiate(boulderPrefab, spawnPos, Quaternion.identity);
            boulder.transform.localScale = new Vector3(boulderSize, boulderSize, 1);

            Rigidbody2D boulderRb = boulder.GetComponent<Rigidbody2D>();
            if (boulderRb != null)
            {
                // Give each boulder a small outward/upward pop so they don't overlap
                Vector2 launchDir = new Vector2(direction, 0.5f).normalized;
                boulderRb.linearVelocity = launchDir * boulderLaunchForce;
            }
        }
    }
}