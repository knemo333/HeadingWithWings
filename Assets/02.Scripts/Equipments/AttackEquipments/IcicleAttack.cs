using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleAttack : Equipment
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
    const string ENEMY = "ENEMY";
    public Bullet bullet;
    public float damageMultiplier;
    public float attackDelayMultiplier;
    public float attackRange;
    public float knockbackSize;
    public float bulletSpeed;
    // Reduce Speed
    public float speedMultiplier;

    private Transform targetTransform;
    private bool isCoolDown = false;


    private void Start()
    {
        Initialize();
        StartCoroutine(FireCycle());
    }

    void Initialize()
    {
        playerInfo = GameManager.playerInfo;
        detectEnemy = GetComponent<DetectEnemy>();
    }

    public void Fire()
    {
        Bullet newBullet = Instantiate(bullet, transform.position, transform.rotation);
        newBullet.transform.LookAt(targetTransform);
        newBullet.damage = playerInfo.damage * damageMultiplier;
        newBullet.knockbackSize = knockbackSize;
        ((Bullet_Icicle)newBullet).speedMultiplier = speedMultiplier;
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * bulletSpeed, ForceMode.Impulse);
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

            if (Vector3.Distance(transform.position, targetTransform.position) > attackRange) continue;

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

        //220527 하드코딩이므로 DataManager 이용할 것.
        switch (level)
        {
            case 1:
                {
                    damageMultiplier = 0.05f;
                    attackDelayMultiplier = 3.00f;
                    speedMultiplier = 0.95f;
                    break;
                }
            case 2:
                {
                    damageMultiplier = 0.10f;
                    attackDelayMultiplier = 2.95f;
                    speedMultiplier = 0.90f;
                    break;
                }
            case 3:
                {
                    damageMultiplier = 0.15f;
                    attackDelayMultiplier = 2.90f;
                    speedMultiplier = 0.85f;
                    break;
                }
            case 4:
                {
                    damageMultiplier = 0.20f;
                    attackDelayMultiplier = 2.85f;
                    speedMultiplier = 0.80f;
                    break;
                }
            case 5:
                {
                    damageMultiplier = 0.25f;
                    attackDelayMultiplier = 2.80f;
                    speedMultiplier = 0.00f;
                    break;
                }
            default:
                break;
        }
    }
}
