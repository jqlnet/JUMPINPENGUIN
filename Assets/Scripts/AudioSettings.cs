using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private static readonly string backgroundPref = "BackgroundPref";
    private float backgroundFloat;
    public AudioSource backgroundAudio;
    void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        backgroundFloat = PlayerPrefs.GetFloat(backgroundPref);

        backgroundAudio.volume = backgroundFloat;
    }

    public void OnSliderChange(float value)
    {
        backgroundAudio.volume = value;
        PlayerPrefs.SetFloat("BackgroundPref", value);
        PlayerPrefs.Save();
    }
}