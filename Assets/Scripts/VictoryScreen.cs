using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System.Collections;


public class VictoryScreen : MonoBehaviour


{
    public GameObject inGameUI;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI difficultyText;

    public Leaderboard leaderboard;
public IEnumerator GameWon()
    {
        inGameUI.SetActive(false);
        gameObject.SetActive(true);

        float completionTime = inGameUI.GetComponent<InGameUI>().getTimer();
        int score = (int)(completionTime * 1000f);
        yield return leaderboard.SubmitScoreRoutine(score);

        int minutes = Mathf.FloorToInt(completionTime / 60f);
        int seconds = Mathf.FloorToInt(completionTime % 60f);
        int milliseconds = Mathf.FloorToInt((completionTime * 1000f) % 1000f);
        timeText.text = $"{minutes:D2}:{seconds:D2}:{milliseconds:D3}";

        float rate = Difficulty.staminaDrainRate;
        string name = "Medium";
        if (rate == 5f) name = "Easy";
        else if (rate == 10f) name = "Hard";
        difficultyText.text = name;
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
