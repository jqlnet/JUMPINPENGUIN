using UnityEngine;
using TMPro;

public class InGameUI : MonoBehaviour
{
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI timerText;
    [SerializeField] PlayerMovement player;

    private float timer = 0f;
    private bool timerStarted = false;

    void Update()
    {
        if (!timerStarted && (
            Input.anyKeyDown ||
            Input.GetAxisRaw("Horizontal") != 0 ||
            Input.GetAxisRaw("Vertical") != 0
        ))
        {
            timerStarted = true;
        }

        if (timerStarted)
        {
            timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            int milliseconds = Mathf.FloorToInt((timer * 1000f) % 1000f);
            timerText.text = $"{minutes:D2}:{seconds:D2}:{milliseconds:D3}";
        }

        foodText.text = ": " + player.foodsCollected.ToString() + " / 10";
    }

    public float getTimer()
    {
        return timer;
    }
}
