using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtonDown : MonoBehaviour
{
    public GameObject mainUI;
    public bool boolOne = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonMain()
    {
        mainUI.SetActive(boolOne);
    }
}
