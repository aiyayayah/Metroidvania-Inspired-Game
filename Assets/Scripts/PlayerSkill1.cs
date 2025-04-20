using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill1 : MonoBehaviour
{
    public float speed = 1.0f;
    public GameObject attackRotation;
    void Start()
    {
        
    }

    void Update()
    {
        if(attackRotation.transform.rotation.y >= 0 && attackRotation.transform.rotation.y < 180) //
        {
            transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime); // +ve to right
            //Time.deltaTime means change per frame to per second
        }
        else
        {
            transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime); // -ve to left
            //Time.deltaTime means change per frame to per second
        }

    }
}
