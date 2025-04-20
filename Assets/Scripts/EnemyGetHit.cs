using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHit : MonoBehaviour
{

    public Animator enemyAnim;

    private void OnTriggerEnter2D(Collider2D collision) //Sent when another object enters a trigger collider attached to this object
    {
        if(collision.tag == "PlayerAttack")
        {
            enemyAnim.SetTrigger("getHit");
        }
        
    }

}
