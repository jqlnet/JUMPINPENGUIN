using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartHotkey : MonoBehaviour
{
    public GameObject victoryScreen;
    public GameObject lossScreen;
    public GameObject pauseMenuPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            bool inGameplay = 
                !(victoryScreen != null && victoryScreen.activeInHierarchy) &&
                !(lossScreen != null && lossScreen.activeInHierarchy) &&
                !(pauseMenuPanel != null && pauseMenuPanel.activeInHierarchy);

            if (inGameplay)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
