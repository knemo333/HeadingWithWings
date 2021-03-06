using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleAttack : Equipment
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
    const int equipID = 10400;
    public float damageMultiplier;
    public float attackDelayMultiplier;
    public float attackRange;
    public float knockbackSize;
    public float bulletSpeed;
    // Reduce Speed
    public float speedMultiplier;
    public float slowDuration;

    private Transform targetTransform;
    private bool isCoolDown = false;


    private void Start()
    {
        Initialize();
        StartCoroutine(FireCycle());
    }

    void Initialize()
    {
        playerInfo = GameObject.FindWithTag(PLAYER).GetComponent<PlayerInfo>();
        detectEnemy = GetComponent<DetectEnemy>();
    }

    public void Fire()
    {
        Bullet bullet = GetBullet(IcicleBulletPool.Instance);
        bullet.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        bullet.damage = playerInfo.damage * damageMultiplier;
        bullet.knockbackSize = knockbackSize;
        bullet.transform.rotation = Utilities.LookAt2(this.transform, targetTransform);
        ((Bullet_Icicle)bullet).speedMultiplier = speedMultiplier;
        ((Bullet_Icicle)bullet).slowDuration = slowDuration;
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bulletSpeed, ForceMode2D.Impulse);
        isCoolDown = true;
        StartCoroutine(CoolDown());
    }

    IEnumerator FireCycle()
    {
        while (true)
        {
            yield return null;
            targetTransform = detectEnemy.FindNearestEnemy(ENEMY);

            if (targetTransform == transform) continue;

            if (Vector2.Distance(transform.position, targetTransform.position) > attackRange) continue;

            if (!isCoolDown)
            {
                Fire();
            }
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(playerInfo.attackDelay * attackDelayMultiplier);
        isCoolDown = false;
    }

    public override void SetLevel(int newLevel)
    {
        this.level = newLevel;
        damageMultiplier = GameManager.Data.AttackEquipDict[equipID + this.level].damageMultiplier;
        attackDelayMultiplier = GameManager.Data.AttackEquipDict[equipID + this.level].delayMultiplier;
        knockbackSize = GameManager.Data.AttackEquipDict[equipID + this.level].knockBackSize;
        speedMultiplier = GameManager.Data.AttackEquipDict[equipID + this.level].speedMultiplier;
        slowDuration = GameManager.Data.AttackEquipDict[equipID + this.level].slowDuration;
    }
}
