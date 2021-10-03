using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public float defaultVolume = .5f;
    public AudioMixer mixer;
    public Slider slider;
    public string savename = "MusicVolume";

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        slider.value = PlayerPrefs.GetFloat(savename, defaultVolume);
        float sliderValue = slider.value;
        mixer.SetFloat(savename, Mathf.Log10(sliderValue) * 20);
    }

    public void SetLevel()
    {
        float sliderValue = slider.value;
        mixer.SetFloat(savename, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(savename, sliderValue);
    }
}