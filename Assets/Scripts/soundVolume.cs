using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundVolume : MonoBehaviour
{
    public static float Audio = 0.8f;
    public static float Music = 0.5f;

    public Slider sliderAudio;
    public Slider sliderMusic;
    public AudioSource audioSourceMain;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void AudioChange()
    {
        Audio = sliderAudio.value;
    }
    public void MusicChange()
    {
        Music = sliderMusic.value;
        audioSourceMain.volume = Music;
    }
}
