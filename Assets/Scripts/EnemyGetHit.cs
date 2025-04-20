using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHit : MonoBehaviour
{
    [Header("Component")]
    public Animator enemyAnim;


    [Header("Get Hit")]
    public GameObject getHitEffect;
    public GameObject getHitPosition;
    private void OnTriggerEnter2D(Collider2D collision) //Sent when another object enters a trigger collider attached to this object
    {
        GetHit(collision);
    }

    public void GetHit(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack")
        {
            enemyAnim.SetTrigger("getHit");

            Instantiate(getHitEffect, getHitPosition.transform.position, getHitPosition.transform.rotation);
        }
    }

}
