using UnityEngine;
using TMPro;

public class DifficultyCheck : MonoBehaviour
{
    public TextMeshProUGUI difficultyText;

    void Start()
    {
            float rate = Difficulty.staminaDrainRate;

            string name = "Medium";
            if (rate == 5f) name = "Easy";
            else if (rate == 10f) name = "Hard";
            difficultyText.text = "Difficulty: " + name;
        }
    }


