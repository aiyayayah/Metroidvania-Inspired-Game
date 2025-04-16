using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnd : MonoBehaviour
{
    [Header("Component")]
    public Animator playerAnim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            playerAnim.SetBool("isJumping", false);
        }
    }
}
