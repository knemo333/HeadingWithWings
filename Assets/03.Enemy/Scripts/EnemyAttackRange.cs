using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : EnemyRangeAttackBase
{
    private Transform playerTransform;
    private EnemyInfo enemyInfo;
    private IEnemyStopHandler stopHandler;
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
        EnemyProjectile projectile = GetProjectile(BasicProjectilePool.Instance);
        projectile.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
        projectile.damage = enemyInfo.projectileDamage;
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        projectile.transform.right = direction;
        projectile.GetComponent<Rigidbody2D>().AddForce(projectile.transform.right * enemyInfo.projectileSpeed, ForceMode2D.Impulse);
    }

    // IEnumerator FireCycle()
    // {
    //     while(isInRange)
    //     {
    //         yield return null;
    //         if(!isAttacking)
    //         {
    //             isAttacking = true;
    //             Fire();
    //             StartCoroutine(FireDelay());
    //         }
    //     }
    // }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(enemyInfo.projectileFireDelay);
        isAttacking = false;
    }
}
