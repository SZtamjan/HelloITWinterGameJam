using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider musicSlider;
    
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip menuSong;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }
    }

    public void ChangeSliderValue(float deltaValue)
    {
        float volume = musicSlider.value;
        volume += deltaValue;
        musicSlider.value = volume;
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        _audioMixer.SetFloat("Music", Mathf.Log10(volume)*20f);
        PlayerPrefs.SetFloat("Music",volume);
    }

    private void LoadVolume()
    {
        float volume = PlayerPrefs.GetFloat("Music");
        _audioMixer.SetFloat("Music", Mathf.Log10(volume)*20f);
    }
    
}
