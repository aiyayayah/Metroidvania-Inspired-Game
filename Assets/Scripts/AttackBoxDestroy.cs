using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoxDestroy : MonoBehaviour
{
    public float destroyTime = 1f;
    void Start()
    {
        Destroy(gameObject, destroyTime); //destroy the attack effect anim after 1 second it created
    }
}
