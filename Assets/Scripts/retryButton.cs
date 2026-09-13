using UnityEngine;
using UnityEngine.SceneManagement;

public class retryButton : MonoBehaviour
{
    public void doExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void reloadScene()
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }
}
