using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 1.0f;
    public float jumpForce = 10.0f;

    public Rigidbody2D playerRig;
    public Animator playerAnim;
    void Start()
    {
        
    }


    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        playerRig.velocity = new Vector2(xInput * speed, playerRig.velocity.y);

        if (xInput != 0) //pressed 
        {
            //playerAnim.Play("Run");
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            //playerAnim.Play("Idle");
            playerAnim.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRig.velocity = new Vector2(playerRig.velocity.x, jumpForce);
        }
    }
}
