using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackMultiShot : EnemyRangeAttackBase
{
    private Transform playerTransform;
    private EnemyInfo enemyInfo;
    private IEnemyStopHandler stopHandler;
    public int projectileCount = 3;
    public float projectileSpread = 30;
    private bool isAttacking;
    private bool isInRange = false;
    public bool IsInRange
    {
        get
        {
            return isInRange;
        }
        set
        {
            isInRange = value;
        }
    }

    private void Start()
    {
        Initialize();
        StartCoroutine(CheckRange());
    }

    private void Update()
    {
        if(IsInRange)
        {
            stopHandler.StopMove();

            if(isAttacking) return;
            
            isAttacking = true;
            Fire();
            StartCoroutine(FireDelay());
        }
        else if(!IsInRange && stopHandler.IsStop)
        {
            stopHandler.ResumeMove();
        }
    }

    void Initialize()
    {
        playerTransform = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        enemyInfo = GetComponent<EnemyInfo>();
        stopHandler = GetComponent<IEnemyStopHandler>();
        //attackRange = GameManager.Data.MonsterDict[enemyInfo.monsterID];
        //fireDelay = GameManager.Data.MonsterDict[int.Parse(gameObject.name)].projectileFireDelay;
        isInRange = false;
        isAttacking = false;
    }

    IEnumerator CheckRange()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.1f);
        while(!enemyInfo.IsDead)
        {
            yield return waitTime;
            if(Vector2.Distance(transform.position, playerTransform.position) <= enemyInfo.attackRange)
            {
                IsInRange = true;
            }
            else
            {
                IsInRange = false;
            }
        }
    }

    void Fire()
    {
        for(int i = 0; i < projectileCount; i++)
        {
            EnemyProjectile projectile = GetProjectile(BasicProjectilePool.Instance);
            projectile.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
            projectile.damage = enemyInfo.projectileDamage;
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            projectile.transform.right = direction;
            projectile.transform.Rotate(0, 0, i * (projectileSpread / projectileCount) - (projectileSpread / 2));
            projectile.GetComponent<Rigidbody2D>().AddForce(projectile.transform.right * enemyInfo.projectileSpeed, ForceMode2D.Impulse);
        }
    }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(enemyInfo.projectileFireDelay);
        isAttacking = false;
    }
}
