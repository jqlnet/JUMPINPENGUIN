using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartHotkey : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads the current scene
        }
    }
}

