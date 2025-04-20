using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1Destroy : MonoBehaviour
{
    public GameObject attackObject;
    public float destroyTime = 0.0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Destroy(attackObject, destroyTime);
        }
    }
}
