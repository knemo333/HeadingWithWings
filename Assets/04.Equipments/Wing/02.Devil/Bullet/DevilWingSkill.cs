using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilWingSkill : EffectBullet
{
    const string ENEMY = "ENEMY";
    private PlayerInfo playerInfo = null;

    private void Start() {
        playerInfo = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<PlayerInfo>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ENEMY))
        {
            HitEffect(BasicEffectPool.Instance, other.transform.position);
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
            playerInfo.HealthPoint += damage * 0.01f * playerInfo.healAmount;
        }
    }
}
