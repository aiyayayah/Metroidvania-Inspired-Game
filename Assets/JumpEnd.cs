using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnd : MonoBehaviour
{
    [Header("Component")]
    public Animator playerAnim;
    
    private void OnTriggerStay2D(Collider2D collision) //Sent once per physics update when another object is within a trigger collider attached to this object (2D physics only). changed from onTriggerEnter2D
    {
        if(collision.tag == "Ground")
        {
            playerAnim.SetBool("isJumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //Sent when another object leaves a trigger collider attached to this object (2D physics only).
    {
        playerAnim.SetBool("isJumping", true);
    }
}
