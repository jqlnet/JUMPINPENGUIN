using UnityEngine;
using TMPro; 

public class InGameUI : MonoBehaviour
{
    public TextMeshProUGUI foodText;
    [SerializeField] PlayerMovement player;

    void Update()
    {
        foodText.text = ": " + player.foodsCollected.ToString();
    }
}
