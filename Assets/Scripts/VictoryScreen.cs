using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class VictoryScreen : MonoBehaviour
{
    public GameObject inGameUI;
    public InGameUI ui;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI difficultyText;

    public void gameWon()
    {
        inGameUI.SetActive(false);
        gameObject.SetActive(true);

        float completionTime = ui.getTimer();
        int minutes = Mathf.FloorToInt(completionTime / 60f);
        int seconds = Mathf.FloorToInt(completionTime % 60f);
        int milliseconds = Mathf.FloorToInt((completionTime * 1000f) % 1000f);
        timeText.text = string.Format("{0:D2}:{1:D2}:{2:D3}", minutes, seconds, milliseconds);

        float rate = Difficulty.staminaDrainRate;
        string difficultyName = "Medium";
        if (Mathf.Approximately(rate, 5f)) difficultyName = "Easy";
        else if (Mathf.Approximately(rate, 10f)) difficultyName = "Hard";
        difficultyText.text = difficultyName;
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

