using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHit : MonoBehaviour
{
    [Header("Component")]
    public Animator playerAnim;
    public AudioSource conSound;
    public float soundVolume = 0.5f;
    public Player playerClass;

    [Header("Get Hit")]
    public GameObject getHitEffect;
    public GameObject getHitPosition;
    public AudioClip getHitAudio;
    public float getHitTime = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision) //Sent when another object enters a trigger collider attached to this object
    {
        GetHit(collision);
    }

    public void GetHit(Collider2D collision)
    {
        if (collision.tag == "EnemyAttack")
        {
            playerAnim.SetTrigger("getHit");
            conSound.PlayOneShot(getHitAudio, soundVolume);
            playerClass.isAttack = true;
            Invoke("EndGetHit", getHitTime);
            Instantiate(getHitEffect, getHitPosition.transform.position, getHitPosition.transform.rotation);

        }
    }

    public void EndGetHit()
    {
        playerClass.isAttack = false;
    }
}
