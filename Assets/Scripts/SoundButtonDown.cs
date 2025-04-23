using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButtonDown : MonoBehaviour
{

    public GameObject soundUI;
    public bool soundTrue = false;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void SoundDown()
    {
        if(soundTrue == true)
        {
            soundUI.SetActive(false);
            soundTrue = false;
        }
        else
        {
            soundUI.SetActive(true);
            soundTrue = true;
        }
    }
}
