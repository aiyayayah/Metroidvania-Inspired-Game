using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHit : MonoBehaviour
{
    [Header("Component")]
    public Animator enemyAnim;
    public AudioSource conSound;
    public EnemyController enemyClass;

    [Header("Get Hit")]
    public GameObject getHitEffect;
    public GameObject getHitPosition;
    public AudioClip getHitAudio;
    public float stunnedTimeAfterHitted = 1.0f;
    public bool isDead = false;

    private void OnTriggerEnter2D(Collider2D collision) //Sent when another object enters a trigger collider attached to this object
    {
        GetHit(collision);
    }
    public void GetHit(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack")
        { 
            ControlHP();
        }
    }
    public void InvokeGetHit()
    {
        enemyClass.enemyState = EnemyState.moveToPlayer;
    }

    public void ControlHP()
    {
        enemyClass.currentHP  = enemyClass.currentHP -  20;

        if (enemyClass.currentHP > 0)
        {
            conSound.PlayOneShot(getHitAudio, soundVolume.Audio);

            enemyClass.enemyState = EnemyState.getHit;

            CancelInvoke("InvokeGetHit");

            Invoke("InvokeGetHit", stunnedTimeAfterHitted);

            enemyAnim.SetTrigger("getHit");

            Instantiate(getHitEffect, getHitPosition.transform.position, getHitPosition.transform.rotation);
        }
        else
        {
            if(isDead == false)
            {
                conSound.PlayOneShot(getHitAudio, soundVolume.Audio);
                CancelInvoke("InvokeGetHit");
                isDead = true;
                enemyAnim.SetTrigger("dead");
                enemyClass.enemyState = EnemyState.dead;
                Instantiate(getHitEffect, getHitPosition.transform.position, getHitPosition.transform.rotation);
            }
        }
    }

}
