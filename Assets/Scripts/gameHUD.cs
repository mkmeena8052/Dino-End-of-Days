using UnityEngine;

public class gameHUD : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public void PauseMenu()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        } else
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
    }
}
