using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void gameLost()
    {
        gameObject.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(1);
    }
    
        public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
