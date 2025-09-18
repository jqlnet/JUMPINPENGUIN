using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource bgmSource;

    void Awake()
    {
        if (FindObjectsOfType<MusicManager>().Length > 1)
        {
            Destroy(gameObject); // Prevent duplication
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
