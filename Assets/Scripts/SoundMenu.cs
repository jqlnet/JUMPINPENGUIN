using UnityEngine;
using UnityEngine.UI;

public class SoundMenuController : MonoBehaviour
{
    public GameObject soundMenuPanel;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource musicSource;

public void OpenSoundSettings()
{
    soundMenuPanel.SetActive(true);

    volumeSlider = soundMenuPanel.GetComponentInChildren<Slider>();
    if (volumeSlider == null)
    {
        Debug.LogError("Volume Slider not found in SoundMenuPanel!");
        return;
    }
    volumeSlider.onValueChanged.RemoveAllListeners();
    volumeSlider.onValueChanged.AddListener(SetVolume);

    // Always load slider from PlayerPrefs
    volumeSlider.value = PlayerPrefs.GetFloat("soundVolume", 1f);

    // And update music volume accordingly
    if (musicSource != null)
        musicSource.volume = volumeSlider.value;
}



void Awake()
{
    if (FindObjectsOfType<SoundMenuController>().Length > 1)
    {
        Destroy(gameObject);
        return;
    }
    DontDestroyOnLoad(gameObject);
}

void Start()
{
    if (musicSource == null)
        musicSource = GetComponent<AudioSource>();

    if (volumeSlider != null)
        volumeSlider.onValueChanged.AddListener(SetVolume);

    if (PlayerPrefs.HasKey("soundVolume"))
        loadVolume();
    else
    {
        PlayerPrefs.SetFloat("soundVolume", 1);
        loadVolume();
    }

    if (musicSource != null && volumeSlider != null)
        musicSource.volume = volumeSlider.value;
}


    public void SetVolume(float value)
    {
        if (musicSource != null)
            musicSource.volume = value;

        SaveVolume();
    }

    void SaveVolume()
    {
        if (volumeSlider != null)
            PlayerPrefs.SetFloat("soundVolume", volumeSlider.value);
    }

    void loadVolume()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("soundVolume");
            if (musicSource != null)
                musicSource.volume = volumeSlider.value;
        }
    }
}
