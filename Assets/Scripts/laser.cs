using UnityEngine;

public class laser : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create;


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerMovement>().gameOver();
        }
    }
}
