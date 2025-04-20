using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class PlayerSkill2 : MonoBehaviour
{
    public float startDelayTime = 0.5f;
    public float repeatTime = 0.5f;  // more high more hits
    public GameObject damageBox;
    public GameObject endPosition;
    public GameObject startPosition;
    void Start()
    {
        InvokeRepeating("CreateDamageBox", startDelayTime, repeatTime);
    }

    public void CreateDamageBox()
    {
        GameObject box = Instantiate(damageBox, startPosition.transform.position, startPosition.transform.rotation);
        box.transform.DOMove(endPosition.transform.position, repeatTime);
     
    }
}
