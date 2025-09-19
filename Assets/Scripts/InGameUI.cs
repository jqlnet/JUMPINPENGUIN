using UnityEngine;
using TMPro;

public class InGameUI : MonoBehaviour
{
    public TextMeshProUGUI foodText;

    [SerializeField] PlayerMovement player;

    private void Start()
    {
        foodText.text = "Gems Collected: " + player.foodsCollected;
    }

    private void Update()
    {
        foodText.text = "Gems Collected: " + player.foodsCollected;
    }
}