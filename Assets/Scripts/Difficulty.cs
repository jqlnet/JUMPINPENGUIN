using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public static Difficulty Instance;

    public float staminaDrainRate = 8f;
    public GameObject difficultyPanel;
    public GameObject staminaBar;
    public PlayerMovement playerMovement;
    public GameObject inGameUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Instance = this;
        }

        ShowDifficultyPanel();
    }

    public void ShowDifficultyPanel()
    {
        if (difficultyPanel != null)
            difficultyPanel.SetActive(true);

        if (staminaBar != null)
            staminaBar.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (inGameUI != null)
            inGameUI.SetActive(false);
        else
            Debug.LogError("Difficulty: playerMovement missing from inspector!");
    }

    public void SetEasy()
    {
        staminaDrainRate = 5f;
        HideDifficultyPanel();
    }

    public void SetMedium()
    {
        staminaDrainRate = 7.5f;
        HideDifficultyPanel();
    }

    public void SetHard()
    {
        staminaDrainRate = 10f;
        HideDifficultyPanel();
    }

    private void HideDifficultyPanel()
    {
        if (difficultyPanel != null)
            difficultyPanel.SetActive(false);

        if (staminaBar != null)
            staminaBar.SetActive(true);

        if (playerMovement != null)
            playerMovement.enabled = true;

        if (inGameUI != null)
            inGameUI.SetActive(true);
        else
            Debug.LogError("Difficulty: playerMovement missing from inspector!");
    }
}
