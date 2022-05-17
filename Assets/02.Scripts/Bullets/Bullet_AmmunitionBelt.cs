using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_AmmunitionBelt : Bullets
{
    const string ENEMY = "ENEMY";
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ENEMY)
        {
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, 10);
            Destroy(gameObject);
        }
    }
}