using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class InGameAudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    
    private void Start()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            LoadVolume();
        }
    }
    
    private void LoadVolume()
    {
        float volume = PlayerPrefs.GetFloat("Music");
        _audioMixer.SetFloat("Music", Mathf.Log10(volume)*20f);
    }
    
    
}
