using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour


{
    public GameObject inGameUI;
    public void gameWon()
    {
        inGameUI.SetActive(false);
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
