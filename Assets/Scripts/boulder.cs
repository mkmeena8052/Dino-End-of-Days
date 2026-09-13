using UnityEngine;

public class Boulder : MonoBehaviour
{
    // Time in seconds before the boulder disappears
    public float n = 5f;
    [SerializeField] private Sprite[] sprites;

    void Awake()
    {
        transform.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }
    void Start()
    {
        Invoke(nameof(RemoveBoulder), n);
    }

    void RemoveBoulder() { Destroy(gameObject); }
}