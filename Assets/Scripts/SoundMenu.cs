using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundMenuController : MonoBehaviour
{
    public GameObject soundMenuPanel;
    [SerializeField] private Slider volumeSlider;
    private AudioSource musicSource;

    public void OpenSoundSettings()
    {
        //mainMenuPanel.SetActive(false);
        soundMenuPanel.SetActive(true);
    }

    void Start()
    {
        // Find the MusicManager in the scene
        MusicManager mgr = FindObjectOfType<MusicManager>();
        if (mgr != null)
        {
            musicSource = mgr.bgmSource;
            volumeSlider.value = musicSource.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    void SetVolume(float value)
    {
        if (musicSource != null)
            musicSource.volume = value;
    }
}
