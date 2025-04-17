using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    #region parameters
    [Header("Character")]
    public float speed = 1.0f;
    public float jumpForce = 5.0f;

    [Header("Component")]
    public Rigidbody2D playerRig;
    public Animator playerAnim;
    public GameObject playerModel;
    #endregion

    void Start()
    {
        
    }


    void Update()
    {
        Move();
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision) //trigger when an incoming collider makes contact with this object's collider (2D physics only).
    {
        Debug.Log("Collide");
        playerAnim.SetBool("isJumping", false);

    }
    #region CharacterControl

    public void Move()
    {
        //get user input
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        //move left and right
        playerRig.velocity = new Vector2(xInput * speed, playerRig.velocity.y);
        playerAnim.SetFloat("RunBlend", Mathf.Abs(xInput));

        //animation
        if (xInput > 0) //move to right 
        {
            playerModel.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (xInput < 0) // move to lefy
        {
            playerModel.transform.rotation = Quaternion.Euler(0, 180, 0); //flip
        }
    }

    public void Jump()
    {
        bool isJump = playerAnim.GetBool("isJumping");
        if (Input.GetKeyDown(KeyCode.Space) && isJump == false)
        {
            playerRig.velocity = new Vector2(playerRig.velocity.x, jumpForce);
        }
    }
    #endregion
}
