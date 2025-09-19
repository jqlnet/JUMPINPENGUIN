using UnityEngine;
using UnityEngine.SceneManagement;

public class Mmenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
