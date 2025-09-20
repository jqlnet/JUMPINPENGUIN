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
        timerText.text = $": {timer:F2} s";

        foodText.text = ": " + player.foodsCollected.ToString() + " / 9";
    }
}
