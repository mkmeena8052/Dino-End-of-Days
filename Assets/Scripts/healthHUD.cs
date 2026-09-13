using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite brokenHeart;
    [SerializeField] private int maxHealth;

    public void UpdateHealth(int health)
    {
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < health) hearts[i].sprite = fullHeart;
            else hearts[i].sprite = brokenHeart;
        }
    }
}
