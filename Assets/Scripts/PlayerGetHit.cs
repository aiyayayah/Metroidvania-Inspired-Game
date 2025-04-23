using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHit : MonoBehaviour
{
    [Header("Component")]
    public Animator playerAnim;
    public AudioSource conSound;
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
       if(collision.tag == "EnemyAttack")
        {
            if(!playerClass.shieldCon)
            {
                ControlHP();
            }   
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
            playerAnim.SetTrigger("GetHit");
            conSound.PlayOneShot(getHitAudio, soundVolume.Audio);
            playerClass.isAttack = true;
            CancelInvoke("EndGetHit");
            Invoke("EndGetHit", getHitTime);
            Instantiate(getHitEffect, getHitPosition.transform.position, getHitPosition.transform.rotation);
        }
        else
        {
            if(isDead == false)
            {
                conSound.PlayOneShot(getHitAudio, soundVolume.Audio);
                isDead = true;
                CancelInvoke("EndGetHit");
                playerClass.isAttack = true;
                playerAnim.SetTrigger("Die");
                Instantiate(getHitEffect, getHitPosition.transform.position, getHitPosition.transform.rotation);
            }
        }
    }

}
