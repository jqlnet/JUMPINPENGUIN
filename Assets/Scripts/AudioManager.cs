using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    private static readonly string firstPlay = "FirstPlay";
    private static readonly string backgroundPref = "BackgroundPref";
    private int firstPlayInt;

    public Slider backgroundSlider;
    private float backgroundFloat;

    public AudioSource backgroundAudio;

    public static AudioManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    { // check to see if its first playthrough
        firstPlayInt = PlayerPrefs.GetInt(firstPlay);

        backgroundSlider.onValueChanged.AddListener(OnSliderChange);

        // notes for the future on audio initialization
        //Start() or initialization logic in AudioManager
        //sets both backgroundSlider.value and backgroundAudio.volume to the saved PlayerPrefs value at launch.
        // this is bad so we need to make sure the listener is selecting the sliderchange over the default bgm.volume

        if (firstPlayInt == 0)
        {
            backgroundFloat = .5f;
            backgroundSlider.value = backgroundFloat;
            PlayerPrefs.SetFloat(backgroundPref, backgroundFloat);
            PlayerPrefs.SetInt(firstPlay, -1); // only sets it to one the first time.
        }
        else
        {
            backgroundFloat = PlayerPrefs.GetFloat(backgroundPref);
            backgroundSlider.value = backgroundFloat;
        }

        float savedVolume = PlayerPrefs.GetFloat("BackgroundPref", 0.5f);
        backgroundAudio.volume = savedVolume;
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(backgroundPref, backgroundSlider.value);
    }

    void OnApplicationFocus(bool inFocus)
    {

        if (!inFocus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        backgroundAudio.volume = backgroundSlider.value;
    }
    public void OnSliderChange(float value)
    {
        Debug.Log(value);
        AudioManager.Instance.backgroundAudio.volume = value;
        PlayerPrefs.SetFloat("BackgroundPref", value);
        PlayerPrefs.Save();
    }
}