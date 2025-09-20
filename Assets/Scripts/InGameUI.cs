using UnityEngine;
using TMPro;

public class InGameUI : MonoBehaviour
{
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI timerText;
    [SerializeField] PlayerMovement player;

    private float timer = 0f;
    void Update()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        int milliseconds = Mathf.FloorToInt((timer * 1000f) % 1000f);
        timerText.text = $"Timer: {minutes:D2}:{seconds:D2}:{milliseconds:D3}";

        foodText.text = ": " + player.foodsCollected.ToString() + " / 9";
    }
}
