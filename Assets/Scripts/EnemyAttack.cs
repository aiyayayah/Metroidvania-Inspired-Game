using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject attackBox;
    public GameObject attackLocation;
    public void Attack()
    {
        Instantiate(attackBox, attackLocation.transform.position, attackLocation.transform.rotation);
    }
}
