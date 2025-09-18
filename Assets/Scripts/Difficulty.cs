using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public static Difficulty Instance;
    public float staminaDrainRate = 8f;
    public GameObject difficultyPanel; // assign your panel in the inspector
    public GameObject staminaBar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetEasy()
    {
        staminaDrainRate = 3f;
        if (difficultyPanel != null)
            difficultyPanel.SetActive(false);

        if (staminaBar != null)
            staminaBar.SetActive(true);

        GetComponent<PlayerMovement>().enabled = true;


    }

    public void SetMedium()
    {
        staminaDrainRate = 6f;
        if (difficultyPanel != null)
            difficultyPanel.SetActive(false);

        if (staminaBar != null)
            staminaBar.SetActive(true);

        GetComponent<PlayerMovement>().enabled = true;
    }

    public void SetHard()
    {
        staminaDrainRate = 9f;
        if (difficultyPanel != null)
            difficultyPanel.SetActive(false);

        if (staminaBar != null)
            staminaBar.SetActive(true);

        GetComponent<PlayerMovement>().enabled = true;
    }
}