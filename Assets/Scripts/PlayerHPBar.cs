using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    public Player playerClass;
    public Slider mainSlider;

    void Start()
    {
        
    }

    void Update()
    {
        mainSlider.value =  playerClass.currentHP / playerClass.maxHP;
    }
}
