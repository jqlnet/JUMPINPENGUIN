using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public static Difficulty Instance;
    public static float staminaDrainRate = 7.5f;
    public GameObject difficultyPanel;
    [SerializeField] private TMPro.TextMeshProUGUI selectedText;

    // 5.0 = Easy
    // 7.5 = Medium
    // 10.0 = Hard
    private void Start()
    {
        if (staminaDrainRate == 5f)
        {
            if (selectedText != null)
            {
                selectedText.text = "Selected: Easy";
            }
        }
        else if (staminaDrainRate == 7.5f)
        {
            if (selectedText != null)
            {
                selectedText.text = "Selected: Medium";
            }
        }
        else if (staminaDrainRate == 10f)
        {
            if (selectedText != null)
            {
                selectedText.text = "Selected: Hard";
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetEasy()
    {
        staminaDrainRate = 5f;
        Debug.Log("Difficulty set to EASY. Drain rate: " + staminaDrainRate, this);

        // Find parent "Warning" object and change tmp text
        if (selectedText != null)
        {
            selectedText.text = "Selected: Easy";
        }
        else
        {
            Debug.LogWarning("Script not assigned or missing TextMeshProUGUI component.", this);
        }

    }
    public void SetMedium()
    {
        staminaDrainRate = 7.5f;
        Debug.Log("Difficulty set to MEDIUM. Drain rate: " + staminaDrainRate, this);

        if (selectedText != null)
        {
            selectedText.text = "Selected: Medium";
        }
        else
        {
            Debug.LogWarning("Script not assigned or missing TextMeshProUGUI component.", this);
        }
    }
    public void SetHard()
    {
        staminaDrainRate = 10f;
        Debug.Log("Difficulty set to HARD. Drain rate: " + staminaDrainRate, this);

        if (selectedText != null)
        {
            selectedText.text = "Selected: Hard";
        }
        else
        {
            Debug.LogWarning("Script not assigned or missing TextMeshProUGUI component.", this);
        }
    }
}
