using System.Collections;
using System.Collections.Generic;
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

    void Start()
    { // check to see if its first playthrough
        firstPlayInt = PlayerPrefs.GetInt(firstPlay);

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



}