using UnityEngine;
public class PowerUp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Chilli")) collision.gameObject.GetComponent<PlayerMovement>().shootPower = true;
            else if (gameObject.CompareTag("Copter")) collision.gameObject.GetComponent<PlayerMovement>().doubleJumpPower = true;
            Destroy(gameObject);
        }
    }
}
