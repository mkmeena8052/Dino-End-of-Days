using System.Collections;
using UnityEngine;

public class UFOController : MonoBehaviour
{
    public enum UFOState { Wandering, Preparing, Attacking, Leaving }

    [Header("State")]
    public UFOState currentState = UFOState.Wandering;

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float changeDirectionInterval = 2f;
    public float leaveSpeed = 8f;
    public float offScreenHeight = 30f;

    [Tooltip("Keep the UFO within these horizontal boundaries on screen")]
    public float minX = -8f;
    public float maxX = 8f;

    [Header("Attack Settings")]
    public float detectionDistance = 15f;
    public float laserWidth = 1.5f; // Beam width
    public float prepareDelay = 1.5f;
    public float laserDuration = 1f;
    public int maxAttacks = 3;

    [Tooltip("Set this to the Layer your Player is on")]
    public LayerMask playerLayer;

    [Header("References")]
    [SerializeField] private SpriteRenderer laser;

    private int attackCount = 0;
    private float horizontalDirection = 1f; // 1 = right, -1 = left
    private float wanderTimer;
    private bool hasHitPlayerThisAttack = false;

    void Start()
    {
        if (laser != null) laser.enabled = false;
        PickNewWanderDirection();
    }

    void Update()
    {
        switch (currentState)
        {
            case UFOState.Wandering:
                HandleWandering();
                CheckForPlayerUnderneath();
                break;
            case UFOState.Preparing:
                // UFO halts while charging/aiming
                break;
            case UFOState.Attacking:
                FireLaser();
                break;
            case UFOState.Leaving:
                HandleLeaving();
                break;
        }
    }

    private void HandleWandering()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0)
        {
            PickNewWanderDirection();
        }

        // Move strictly Left (-X) and Right (+X)
        transform.Translate(Vector3.right * (horizontalDirection * moveSpeed * Time.deltaTime), Space.World);

        // Reverse direction if reaching horizontal screen limits
        if (transform.position.x <= minX)
        {
            horizontalDirection = 1f;
            wanderTimer = changeDirectionInterval;
        }
        else if (transform.position.x >= maxX)
        {
            horizontalDirection = -1f;
            wanderTimer = changeDirectionInterval;
        }
    }

    private void PickNewWanderDirection()
    {
        // Randomly pick left (-1) or right (1)
        horizontalDirection = (Random.value < 0.5f) ? -1f : 1f;
        wanderTimer = changeDirectionInterval;
    }

    private void CheckForPlayerUnderneath()
    {
        // Physics2D.BoxCast checks a downward rectangle beam of width 'laserWidth'
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position,
            new Vector2(laserWidth, 0.1f),
            0f,
            Vector2.down,
            detectionDistance,
            playerLayer
        );

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            StartCoroutine(AttackSequence());
        }
    }

    private IEnumerator AttackSequence()
    {
        currentState = UFOState.Preparing;
        hasHitPlayerThisAttack = false;

        yield return new WaitForSeconds(prepareDelay);

        currentState = UFOState.Attacking;
        if (laser != null) laser.enabled = true;

        float attackTimer = 0f;
        while (attackTimer < laserDuration)
        {
            attackTimer += Time.deltaTime;
            yield return null;
        }

        if (laser != null) laser.enabled = false;
        attackCount++;

        if (attackCount >= maxAttacks)
        {
            currentState = UFOState.Leaving;
        }
        else
        {
            PickNewWanderDirection();
            currentState = UFOState.Wandering;
        }
    }

    private void FireLaser()
    {
        // Only trigger Game Over once per attack cycle
        if (hasHitPlayerThisAttack) return;

        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position,
            new Vector2(laserWidth, 0.1f),
            0f,
            Vector2.down,
            detectionDistance,
            playerLayer
        );

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            PlayerMovement pm = hit.collider.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                hasHitPlayerThisAttack = true;
                pm.gameOver();
            }
        }
    }

    private void HandleLeaving()
    {
        transform.Translate(Vector3.up * (leaveSpeed * Time.deltaTime), Space.World);

        if (transform.position.y > offScreenHeight)
        {
            Destroy(gameObject);
        }
    }

    // Draws the laser beam detection box in the Scene view for easy debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            transform.position + Vector3.down * (detectionDistance / 2f),
            new Vector3(laserWidth, detectionDistance, 0.1f)
        );

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(minX, transform.position.y - 1f, 0), new Vector3(minX, transform.position.y + 1f, 0));
        Gizmos.DrawLine(new Vector3(maxX, transform.position.y - 1f, 0), new Vector3(maxX, transform.position.y + 1f, 0));
    }
}