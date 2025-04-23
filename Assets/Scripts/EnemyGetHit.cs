using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHit : MonoBehaviour
{
    [Header("Component")]
    public Animator enemyAnim;
    public AudioSource conSound;
    public float soundVolume = 0.5f;
    public EnemyController enemyClass;

    [Header("Get Hit")]
    public GameObject getHitEffect;
    public GameObject getHitPosition;
    public AudioClip getHitAudio;
    public float stunnedTimeAfterHitted = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision) //Sent when another object enters a trigger collider attached to this object
    {
        GetHit(collision);
    }
    public void GetHit(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack")
        { 
            conSound.PlayOneShot(getHitAudio, soundVolume);
            enemyClass.enemyState = EnemyState.getHit;
            CancelInvoke("InvokeGetHit");
            Invoke("InvokeGetHit", stunnedTimeAfterHitted);
            enemyAnim.SetTrigger("getHit");
            Instantiate(getHitEffect, getHitPosition.transform.position, getHitPosition.transform.rotation);
        }
    }
    public void InvokeGetHit()
    {
        enemyClass.enemyState = EnemyState.moveToPlayer;
    }
}
