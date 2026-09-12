using UnityEngine;

public class Boulder : MonoBehaviour
{
    // Time in seconds before the boulder disappears
    public float n = 5f;
    void Start()
    {
        Invoke(nameof(RemoveBoulder), n);
    }

    void RemoveBoulder() { Destroy(gameObject); }
}