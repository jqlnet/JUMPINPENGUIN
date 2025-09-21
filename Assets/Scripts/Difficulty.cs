using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public static Difficulty Instance;
    public static float staminaDrainRate = 7.5f;
    public GameObject difficultyPanel;

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
    }
    public void SetMedium()
    {
        staminaDrainRate = 7.5f;
        Debug.Log("Difficulty set to MEDIUM. Drain rate: " + staminaDrainRate, this);
    }
    public void SetHard()
    {
        staminaDrainRate = 10f;
        Debug.Log("Difficulty set to HARD. Drain rate: " + staminaDrainRate, this);
    }
}
