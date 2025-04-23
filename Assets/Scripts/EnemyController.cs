using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyMoveDirection
{
    patrolIdle,
    moveLeft,
    moveRight
}

public enum EnemyState
{
    patrol,
    moveToPlayer,
    enemyAttack,
    getHit,
    dead
}

public class EnemyController : MonoBehaviour
{

    [Header("PatrolLocation")]
    public GameObject locationLeft;  //left location from zombie
    public GameObject locationCenter; //location on zombie
    public GameObject locationRight; //right location from zombie
    public GameObject locationTarget;

    public float playerEnemyDistance = 0f;  //how far the player is from the enemy
    public float centerDistance = 0f; //how far the player is from the center patrol point
    public float walkDistance = 5f; //enemy start walking if player within this range
    public float attackDistance = 1f; //enemy start attacking if player within this range
    public float standAndStopTime = 3.0f; //how long the enemy stays idle before moving again
    public EnemyMoveDirection moveDirection = EnemyMoveDirection.moveLeft; //initially set the enemy start moving to left when the game begins
    public EnemyState enemyState = EnemyState.patrol;

    [Header("Enemy")]
    public float enemeyMovingSpeed = 1f;
    public float attackTime = 2f;
    public float maxHP = 100f;
    public float currentHP = 100f;

    [Header("EnemyComponent")]
    public Animator enemyAnim;
    public GameObject enemyDestroyObJ;
    private GameObject player;
    private bool canAttack = true;
    private bool isDead = false;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Distance();

        if (enemyState == EnemyState.patrol)
        {
            PatrolMove();
            if(centerDistance <= walkDistance)
            {
                enemyState = EnemyState.moveToPlayer;
            }
        }
        else if (enemyState == EnemyState.moveToPlayer)
        {
            MoveToPlayer();
            if (centerDistance > walkDistance)
            {
                enemyState = EnemyState.patrol;
            }
            else if(playerEnemyDistance <= attackDistance)
            {
                enemyState = EnemyState.enemyAttack;
            }
        }
        else if (enemyState == EnemyState.enemyAttack)
        {
            AttackPlayer();
        }
        else if(enemyState == EnemyState.getHit)
        {
            enemyAnim.SetBool("isRun", false); //idle anim
          
        }
        else if (enemyState == EnemyState.dead)
        { 
            if(isDead == false)
            {
                isDead = true;
                Destroy(enemyDestroyObJ, 4.0f);
            }
        }
    }

    public void Distance()
    {
        //get enemy's own distance and player's distcance
        playerEnemyDistance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(playerEnemyDistance);
        //player atatck at distance 2.5


        //measure distance from the central patrol point to the player
        centerDistance = Vector2.Distance(locationCenter.transform.position, player.transform.position);
        Debug.Log("distance center: " + centerDistance);
    }

    public void PatrolMove()
    {
        if (moveDirection == EnemyMoveDirection.moveLeft)
        {
            locationTarget = locationLeft;
            
            PatrolMoveLeftAndRight();
            if (transform.position.x == locationLeft.transform.position.x)
            {
                moveDirection = EnemyMoveDirection.patrolIdle;
                Invoke("InvokeRight", standAndStopTime);
            }
        }
        else if (moveDirection == EnemyMoveDirection.moveRight)
        {
            locationTarget = locationRight;
            enemyAnim.SetBool("isRun", true);
            PatrolMoveLeftAndRight();
            if (transform.position.x == locationRight.transform.position.x)
            {
                moveDirection = EnemyMoveDirection.patrolIdle;
                Invoke("InvokeLeft", standAndStopTime);
            }
        }
        else // idle
        {
            enemyAnim.SetBool("isRun", false); //run anim --> idle anim
        }
    }
    public void PatrolMoveLeftAndRight()
    {
        Vector3 locationEnd = locationTarget.transform.position; //temp value to store locationTarget
        locationEnd.y = transform.position.y; //keep y value same as enemey;s current y so it wont lari
        enemyAnim.SetBool("isRun", true);
        transform.position = Vector2.MoveTowards(transform.position, locationEnd, enemeyMovingSpeed * Time.deltaTime);

        //multiply with Time.deltaTime bcs if didnt, it will calculate using fps, and each computer have different fps

        Vector3 direction = (locationEnd - transform.position).normalized;

        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void InvokeLeft()
    {
        moveDirection = EnemyMoveDirection.moveLeft;
    }

    public void InvokeRight()
    {
        moveDirection = EnemyMoveDirection.moveRight;
    }

    public void MoveToPlayer()
    {
        enemyAnim.SetBool("isRun", true);
        Vector3 locationEnd = player.transform.position; 
        locationEnd.y = transform.position.y; 

        transform.position = Vector2.MoveTowards(transform.position, locationEnd, enemeyMovingSpeed * Time.deltaTime);

        Vector3 direction = (locationEnd - transform.position).normalized;

        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void AttackPlayer()
    {
        enemyAnim.SetBool("isRun", false);
        if (canAttack == true)
        {  
            canAttack = false;
            enemyAnim.SetTrigger("attack");
            Invoke("AttackEnd", attackTime);
        }
    }

    public void AttackEnd()
    {
        canAttack = true;
        if (playerEnemyDistance > attackDistance)
        {
            enemyState = EnemyState.moveToPlayer;
        }
        else if (centerDistance > walkDistance)
        {
            enemyState = EnemyState.patrol;
        }
    }
}
