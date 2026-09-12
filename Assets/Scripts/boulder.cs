using UnityEngine;

public class Boulder : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;
    Rigidbody2D rigidbody;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // None
    }
}
