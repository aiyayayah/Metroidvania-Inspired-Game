using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHit : MonoBehaviour
{
    [Header("Component")]
    public Animator enemyAnim;
    public AudioSource conSound;
    public float soundVolume = 1.0f;

    [Header("Get Hit")]
    public GameObject getHitEffect;
    public GameObject getHitPosition;
    public AudioClip getHitAudio;

    private void OnTriggerEnter2D(Collider2D collision) //Sent when another object enters a trigger collider attached to this object
    {
        GetHit(collision);
    }

    public void GetHit(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack")
        {
            enemyAnim.SetTrigger("getHit");
            conSound.PlayOneShot(getHitAudio, soundVolume);
            Instantiate(getHitEffect, getHitPosition.transform.position, getHitPosition.transform.rotation);
        }
    }

}
