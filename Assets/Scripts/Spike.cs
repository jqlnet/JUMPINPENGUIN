using UnityEngine;

// Minimal spike: sets player stamina to 0 on contact so existing Update() logic triggers death.
public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var pm = other.GetComponent<PlayerMovement>();
        if (pm != null) 
        {
            pm.SetStamina(0f); // Death will be handled automatically in PlayerMovement.Update()
        }
    }
}
