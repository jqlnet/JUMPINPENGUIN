using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                pauseMenuPanel.SetActive(true);
                Time.timeScale = 0f; // Pauses the game
                isPaused = true;
            }
            else
            {
                pauseMenuPanel.SetActive(false);
                Time.timeScale = 1f; // Resumes the game
                isPaused = false;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }


}
