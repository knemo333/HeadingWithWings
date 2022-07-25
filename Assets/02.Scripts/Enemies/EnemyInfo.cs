using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField]
    private int monsterID;
    public int MonsterID { get { return monsterID; } set { monsterID = value; } }
    private float maxHealthPoint;
    public float healthPoint;
    public float moveSpeed;
    public float meleeDamage;
    public float projectileDamage;
    public float projectileSpeed;
    public float projectileFireDelay;
    public float attackRange;

    private bool isDead;
    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            isDead = value;
            if(value)
            {
                EnemyDie();
            }
        }
    }


    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        playerTransform = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        //monsterID = int.Parse(gameObject.name);
        IsDead = false;
        //healthPoint = GameManager.Data.MonsterDict[monsterID].monsterHp;
        maxHealthPoint = healthPoint;
        //meleeDamage = GameManager.Data.MonsterDict[monsterID].collisionDamage;
        // gameObject.name = crowData.MonsterName;
        // MonsterID = crowData.ID;
        // maxHealthPoint = crowData.HP;
        // meleeDamage = crowData.Damage;
        // enemyMoveSpeed = crowData.MoveSpeed;
    }

    private void FixedUpdate() {
        LookAtPlayer2D();
    }

    private void EnemyDie()
    {
        GameManager.Instance.KillCount++;
        meleeDamage = 0;
        GetComponentInChildren<SpriteRenderer>().material.color = Color.black;
        GetComponent<EnemyDropItem>().DropItem();
        Destroy(gameObject, 0.1f);
    }

    public void ChainDie()
    {
        meleeDamage = 0;
        GetComponentInChildren<SpriteRenderer>().material.color = Color.black;
        Destroy(gameObject, 0.1f);
    }

    private void LookAtPlayer2D()
    {
        float relativeX = transform.position.x - playerTransform.position.x;
        
        if(relativeX > 0)
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        else if(relativeX < 0)
            GetComponentInChildren<SpriteRenderer>().flipX = true;
    }

    public void SumHealthPoint(float value)
    {
        healthPoint += value;
        if(healthPoint > maxHealthPoint)
        {
            healthPoint = maxHealthPoint;
        }
    }
}
