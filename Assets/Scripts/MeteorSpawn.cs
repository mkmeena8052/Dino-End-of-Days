using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private float spawnInterval = 2f;

    private BoxCollider2D spawnArea;
    private float timer;

    void Awake()
    {
        spawnArea = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnMeteor();
        }
    }

    private void SpawnMeteor()
    {
        if (meteorPrefab == null || spawnArea == null)
            return;

        Vector2 spawnPoint = GetRandomPointInBounds();
        Instantiate(meteorPrefab, spawnPoint, Quaternion.identity);
    }

    private Vector2 GetRandomPointInBounds()
    {
        Bounds bounds = spawnArea.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(randomX, randomY);
    }

    // Optional: visualize the spawn area in the editor
    void OnDrawGizmosSelected()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(box.bounds.center, box.bounds.size);
    }
}