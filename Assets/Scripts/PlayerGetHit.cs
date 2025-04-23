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
    public bool isDead = false;

    private void OnTriggerEnter2D(Collider2D collision) //Sent when another object enters a trigger collider attached to this object
    {
        GetHit(collision);
    }

    public void GetHit(Collider2D collision)
    {
        ControlHP();
        if (collision.tag == "EnemyAttack")
        {


        }
    }
    public void EndGetHit()
    {
        playerClass.isAttack = false;
    }

    public void ControlHP()
    {
        playerClass.currentHP = playerClass.currentHP - 20;

        if(playerClass.currentHP > 0)
        {
            playerAnim.SetTrigger("getHit");
            conSound.PlayOneShot(getHitAudio, soundVolume);
            playerClass.isAttack = true;
            CancelInvoke("EndGetHit");
            Invoke("EndGetHit", getHitTime);
            Instantiate(getHitEffect, getHitPosition.transform.position, getHitPosition.transform.rotation);
        }
        else
        {
            if(isDead == false)
            {
                isDead = true;
                CancelInvoke("EndGetHit");
                playerClass.isAttack = true;
                playerAnim.SetTrigger("Die");
                Instantiate(getHitEffect, getHitPosition.transform.position, getHitPosition.transform.rotation);
            }
        }
    }

}
