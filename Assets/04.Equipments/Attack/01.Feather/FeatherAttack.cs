using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherAttack : Equipment
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
    const int equipID = 10100;
    public float damageMultiplier;
    public float attackDelayMultiplier;
    public float attackRange;
    public float knockbackSize;
    public float bulletSpeed;
    public int fireCount = 1;
    public float fireInterval = 0.05f;
    WaitForSeconds waitForInterval;

    private Transform targetTransform;
    private bool isPenetrate = false;
    private bool isCoolDown = false;

    private Color penetrateColor = new Color(0f, 1f, 1f, 1f);

    private void Start()
    {
        Initialize();
        StartCoroutine(FireCycle());
    }

    void Initialize()
    {
        playerInfo = GameObject.FindWithTag(PLAYER).GetComponent<PlayerInfo>();
        detectEnemy = GetComponent<DetectEnemy>();
        waitForInterval = new WaitForSeconds(fireInterval);
    }

    void Fire()
    {
        Bullet bullet = GetBullet(FeatherBulletPool.Instance);
        bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
        bullet.damage = playerInfo.damage * damageMultiplier;
        bullet.knockbackSize = knockbackSize;
        if(isPenetrate)
        {
            SetupPenetrate(bullet);
        }
        bullet.transform.rotation = Utilities.LookAt2(this.transform, targetTransform);
        bullet.GetComponent<Rigidbody2D>().AddForce((targetTransform.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse);
    }
    private void SetupPenetrate(Bullet bullet)
    {
        ((Bullet_PenetrateFeather)bullet).isPenetrate = isPenetrate;
        ((EffectBullet)bullet).effectColor = penetrateColor;
        bullet.GetComponent<SpriteRenderer>().color = penetrateColor;
        bullet.GetComponent<TrailRenderer>().enabled = true;
    }

    IEnumerator FireCycle()
    {
        while(true)
        {
            yield return null;
            if(!isCoolDown)
            {
                targetTransform = detectEnemy.FindNearestEnemy(ENEMY);
                if(Vector2.Distance(transform.position, targetTransform.position) > attackRange) continue;

                for(int i = 0; i < fireCount; i++)
                {
                    isCoolDown = true;
                    Fire();
                    yield return waitForInterval;
                }
                StartCoroutine(CoolDown());
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
        fireCount = GameManager.Data.AttackEquipDict[equipID + this.level].pelletCount;
        // attackRange = GameManager.Data.AttackEquipDict[equipID + this.level].attackRange;
        // bulletSpeed = GameManager.Data.AttackEquipDict[equipID + this.level].bulletSpeed;

        if(newLevel == 5)  isPenetrate = true;
    }
}
