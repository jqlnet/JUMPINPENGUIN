using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public static Difficulty Instance;

    public float staminaDrainRate = 8f;
    public GameObject difficultyPanel; // assign your panel in the inspector
    public GameObject staminaBar;
    public PlayerMovement playerMovement; // assign this in the inspector

    private void Awake()
    {
        // Setup singleton but allow re-init on scene restart
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Instance = this; // Reset the static instance for new scene
        }

        ShowDifficultyPanel(); // Always disable movement on awake
    }

    public void ShowDifficultyPanel()
    {
        // Show selection, disable player movement until a pick
        if (difficultyPanel != null)
            difficultyPanel.SetActive(true);

        if (staminaBar != null)
            staminaBar.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = false;
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
        // Hide selection, enable stamina bar and movement
        if (difficultyPanel != null)
            difficultyPanel.SetActive(false);

        if (staminaBar != null)
            staminaBar.SetActive(true);

        if (playerMovement != null)
            playerMovement.enabled = true;
        else
            Debug.LogError("Difficulty: playerMovement missing from inspector!");
    }
}
