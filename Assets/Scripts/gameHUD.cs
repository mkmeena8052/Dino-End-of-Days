using UnityEngine;
using UnityEngine.SceneManagement;

public class gameHUD : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void doExitGame()
    {
        Application.Quit();
    }

    public void reloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }

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
