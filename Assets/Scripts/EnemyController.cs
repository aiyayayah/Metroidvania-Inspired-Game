using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [Header("PatrolLocation")]
    public GameObject locationLeft;  //left location from zombie
    public GameObject locationCenter; //location on zombie
    public GameObject locationRight; //right location from zombie

    public float enemyDistance = 0f;
    public float centerDistance = 0f;
    public float walkDistance = 5f;
    public float attackDistance = 1f;

    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        //get enemy's own distance and player's distcance
        enemyDistance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(enemyDistance);
        //player atatck at distance 2.5

        centerDistance = Vector2.Distance(locationCenter.transform.position, player.transform.position);
        Debug.Log("distance center: " + centerDistance);

    }
}
