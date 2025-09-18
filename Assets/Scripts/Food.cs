using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private float foodValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        var movement = collision.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.addStamina(foodValue);
            movement.AddFood();
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Food: collided object tagged Player but no PlayerMovement component found on " + collision.name);
        }
    }
}