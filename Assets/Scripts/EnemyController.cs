using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyMoveDirection
{
    idle,
    moveLeft,
    moveRight
}

public class EnemyController : MonoBehaviour
{

    [Header("PatrolLocation")]
    public GameObject locationLeft;  //left location from zombie
    public GameObject locationCenter; //location on zombie
    public GameObject locationRight; //right location from zombie
    public GameObject locationTarget; 

    public float enemyDistance = 0f;  //distance from zombie to player
    public float centerDistance = 0f; //distance from center patrol location to player
    public float walkDistance = 5f; //enemy start walking if player within this range
    public float attackDistance = 1f; //enemy start attacking if player within this range
    public float standAndStopTime = 3.0f;
    public EnemyMoveDirection moveDirection = EnemyMoveDirection.moveLeft;

    [Header("Enemy")]
    public float enemeyMovingSpeed = 1f;

    [Header("EnemyComponent")]
    public Animator enemyAnim;

    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Distance();
        Move();
    }

    public void Distance()
    {
        //get enemy's own distance and player's distcance
        enemyDistance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(enemyDistance);
        //player atatck at distance 2.5


        // Measures distance from the central patrol point to the player
        centerDistance = Vector2.Distance(locationCenter.transform.position, player.transform.position);
        Debug.Log("distance center: " + centerDistance);
    }

    public void Move()
    {
        if(moveDirection == EnemyMoveDirection.moveLeft)
        {
            locationTarget = locationLeft;
            enemyAnim.SetBool("isRun", true);
            MoveLeftAndRight();
            if (transform.position.x == locationLeft.transform.position.x)
            {
                moveDirection = EnemyMoveDirection.idle;
                Invoke("InvokeLeft", standAndStopTime);
            }
        }
        else if (moveDirection == EnemyMoveDirection.moveRight)
        {
            locationTarget = locationRight;
            enemyAnim.SetBool("isRun", true);
            MoveLeftAndRight();
            if (transform.position.x == locationRight.transform.position.x)
            {
                moveDirection = EnemyMoveDirection.idle;
                Invoke("InvokeRight", standAndStopTime);
            }
        }
        else
        {
            enemyAnim.SetBool("isRun", false);
        }
    }
    public void MoveLeftAndRight()
    {
        Vector3 locationEnd = locationTarget.transform.position;
        locationEnd.y = transform.position.y; //keep y value same as enemey;s current y so it wont lari

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
        moveDirection = EnemyMoveDirection.moveRight;
    }

    public void InvokeRight()
    {
        moveDirection = EnemyMoveDirection.moveLeft;
    }
}
