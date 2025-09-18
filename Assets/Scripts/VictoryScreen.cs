using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public void gameWon()
    {
        gameObject.SetActive(true);
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
