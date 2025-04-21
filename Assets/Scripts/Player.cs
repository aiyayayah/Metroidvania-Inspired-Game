using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using static UnityEditor.Sprites.Packer;

public class Player : MonoBehaviour
{
    #region parameters
    [Header("Character")]
    public float speed = 1.0f;
    public float jumpForce = 5.0f;

    [Header("Dash")]
    public bool canDash = true;
    public float dashRead = 1.0f;
    public float dashDirection = 1.0f;
    public float dashInterval = 2.0f;
    public float dashSpeed = 20.0f;
    public bool dashing = false;

    [Header("Normal Attack")]
    public float normalAttackInterval = 1.0f; //CD
    private bool canAttack = true;
    private bool isAttack = false;
    public GameObject attackAnim;
    public GameObject attackLocation;

    [Header("Skill1 Attack")]
    public bool canSkill1 = true;
    public float skill1Read = 1.0f;  //read time before attack 前摇
    public float Skill1Interval = 3.0f; //cd
    public GameObject skill1Box; //collider trigger box
    public GameObject skill1Location; //empty obj

    [Header("Skill2 Attack")]
    public bool canSkill2 = true;
    public float skill2Read = 2.0f;  
    public float Skill2Interval = 5.0f;
    public GameObject skill2Box; 
    public GameObject skill2Location;

    [Header("Shield")]
    public bool canShiled = true;
    public float shieldRead = 1.0f;
    public float shieldInterval = 10.0f;
    public float shieldContinueTime = 2.0f;
    public GameObject shieldBox;
    public GameObject shieldLocation;

    [Header("Component")]
    public Rigidbody2D playerRig;
    public Animator playerAnim;
    public GameObject playerModel;
    public GameObject skillEffect;

    #endregion

    void Start()
    {

    }


    void Update()
    {
        Move();
        Jump();
        Attack();
    }
    private void OnCollisionEnter2D(Collision2D collision) //trigger when an incoming collider makes contact with this object's collider (2D physics only).
    {
        Debug.Log("Collide");
        playerAnim.SetBool("isJumping", false);

    }

    public void Move()
    {
        //get user input
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        if (dashing == true)
        {
            //dash
            playerRig.velocity = new Vector2(dashSpeed * dashDirection, 0); //suppose to be playerRig.velocity.y, but it will cause the player 
        }
        else 
        {
            if (isAttack == true)
            {
                xInput = 0;
            }
            //if isAttack == false,skip block above and the player can move 

            playerRig.velocity = new Vector2(xInput * speed, playerRig.velocity.y); //move left and right
            playerAnim.SetFloat("RunBlend", Mathf.Abs(xInput));

            //animation
            if (xInput > 0) //move to right 
            {
                dashDirection = 1;
                playerModel.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (xInput < 0) // move to left
            {
                dashDirection = -1;
                playerModel.transform.rotation = Quaternion.Euler(0, 180, 0); //flip
            }
        }

    }

    public void Jump()
    {
        if(isAttack == false)
        {
            bool isJump = playerAnim.GetBool("isJumping");
            if (Input.GetKeyDown(KeyCode.Space) && isJump == false)
            {
                playerRig.velocity = new Vector2(playerRig.velocity.x, jumpForce);
            }
        }
    }

    public void Attack()
    {
        if (canAttack == true)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                isAttack = true;
                canAttack = false;

                playerAnim.SetTrigger("NormalAttack");
                Invoke("ResetAttackCoolDown", normalAttackInterval); //delay time 
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                if (canSkill1 == true)
                {
                    canSkill1 = false;
                    canAttack = false;
                    playerAnim.SetTrigger("SkillStart");
                    Invoke("Skill1Attack", skill1Read); //Wait skill1Read seconds before executing the skill
                    LockMovement();
                    SkillStartEffect();

                }
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                if (canSkill2 == true)
                {
                    canSkill2 = false;
                    canAttack = false;
                    playerAnim.SetTrigger("SkillStart");
                    Invoke("Skill2Attack", skill2Read); //Wait skill1Read seconds before executing the skill
                    LockMovement();
                    SkillStartEffect();

                }
            }

            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (canDash == true)
                {
                    canDash = false;
                    canAttack = false;
                    dashing = true;
                    playerAnim.SetTrigger("DashAnimStart");
                    Invoke("ResetAttackCoolDown", normalAttackInterval);
                    Invoke("DashEnd", dashRead); 
                    LockMovement();
                    //SkillStartEffect();
                }
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                if (canShiled == true)
                {
                    canShiled = false;
                    canAttack = false;
                    playerAnim.SetTrigger("SkillStart");
                    Invoke("ShieldAnim", shieldRead);
                    LockMovement();
                    //SkillStartEffect();
                }
            }
        } 
    }
    #region Attack
    public void ResetAttackCoolDown()
    { //AttackEnd()
        canAttack = true;
    }
    public void LockMovement()
    { //AttackStartEvent
        isAttack = true;
    }

    public void UnlockMovement()
    {//AttackEndEvent()
        isAttack = false;
    }

    public void SpawnAttackEffect()
    { //AttackingEvent()
        Instantiate(attackAnim, attackLocation.transform.position, attackLocation.transform.rotation);
    }

    public void SkillStartEffect()
    {
        skillEffect.SetActive(true);
    }

    public void SkillEndEffect()
    {
        skillEffect.SetActive(false);
    }

    #endregion

    #region Skill 1 Attack
    public void Skill1Attack() //actual skill effect happen here
    {
        playerAnim.SetTrigger("SkillEnd"); //end anim
        Instantiate(skill1Box, skill1Location.transform.position, skill1Location.transform.rotation);
        Invoke("ResetAttackCoolDown", normalAttackInterval); //re-enable canAttack = true
        Invoke("Skill1Reset", Skill1Interval); //reset attack1 ability after delay
        UnlockMovement();
        SkillEndEffect();
    }

    public void Skill1Reset()
    {
        canSkill1 = true;
    }
    #endregion

    #region Skill 2 Attack
    public void Skill2Attack()
    {
        playerAnim.SetTrigger("SkillEnd"); 
        Instantiate(skill2Box, skill2Location.transform.position, skill2Location.transform.rotation);
        Invoke("ResetAttackCoolDown", normalAttackInterval); 
        Invoke("Skill2Reset", Skill2Interval); 
        UnlockMovement();
        SkillEndEffect();
    }

    public void Skill2Reset()
    {
        canSkill2 = true;
    }
    #endregion

    #region Dash
    public void DashEnd()
    {
        dashing = false;
        playerAnim.SetTrigger("DashAnimEnd");
        Invoke("DashReset", dashInterval);
        UnlockMovement();
        SkillEndEffect();
    }
    public void DashReset()
    {
        canDash = true;
    }

    #endregion

    #region Shield
    public void ShieldAnim()
    {
        playerAnim.SetTrigger("SkillEnd");
        //Instantiate(shieldBox, shieldLocation.transform.position, shieldLocation.transform.rotation);
        shieldBox.SetActive(true);
        Invoke("ShieldController", shieldContinueTime);
        Invoke("ResetAttackCoolDown", normalAttackInterval);
        Invoke("ShieldReset", shieldInterval);
        UnlockMovement();
        SkillEndEffect();
    }

    public void ShieldReset()
    {
        canShiled = true;
    }

    public void ShieldController()
    {
        shieldBox.SetActive(false);
    }
    #endregion



}


