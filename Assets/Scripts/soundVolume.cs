using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundVolume : MonoBehaviour
{
    public static float Audio = 1.0f;
    public static float Music = 1.0f;
    public Slider sliderAudio;

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
}
