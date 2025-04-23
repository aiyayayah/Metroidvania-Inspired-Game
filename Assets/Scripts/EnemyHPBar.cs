using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public EnemyController enemyClass;
    public Slider mainSlider;

    void Start()
    {

    }

    void Update()
    {
        mainSlider.value = enemyClass.currentHP / enemyClass.maxHP;
    }
}
