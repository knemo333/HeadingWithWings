using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private HealthBar healthBar;
    [SerializeField]
    private float maxHealthPoint;
    public float MaxHealthPoint
    {
        set
        {
            maxHealthPoint = value;
            OnMaxHealthPointChange(maxHealthPoint);
        }
        get
        {
            return maxHealthPoint;
        }
    }

    [SerializeField]
    private float healthPoint;
    public float HealthPoint
    {
        set
        {
            healthPoint = value;
            if(healthPoint > maxHealthPoint)
            {
                healthPoint = maxHealthPoint;
            }
            OnHealthPointChange(healthPoint);
            if(healthPoint <= 0)
            {
                PlayerDie();
            }
        }
        get
        {
            return healthPoint;
        }
    }

    public float maxOxygen;
    public float oxygen;
    public float moveSpeed;
    public float damage;
    public float attackDelay;
    public float attackSize;
    public float itemTakeRange;
    public float healAmount;

    public GameObject[] attackEquipments;
    public int[] abilityEquipments;
    public GameObject wingEquipment;
    public int wingNumber;

    public Transform attackEquipmentsParent;
    public Transform wingEquipmentParent;
    public Transform wingModelParent;

    private void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        int attackEquipmentsCount = System.Enum.GetValues(typeof(AttackEquipmentsNumber)).Length;
        int abilityEquipmentsCount = System.Enum.GetValues(typeof(AbilityEquipmentsNumber)).Length;

        attackEquipments = new GameObject[attackEquipmentsCount];
        abilityEquipments = new int[abilityEquipmentsCount];
    }

    private void OnMaxHealthPointChange(float maxHealthPoint)
    {
        healthBar.SetMaxHealth(maxHealthPoint);
    }
    private void OnHealthPointChange(float healthPoint)
    {
        healthBar.SetHealth(healthPoint);
    }
    
    
    private void PlayerDie()
    {
        // GameObject.FindWithTag("GAMEMANAGER").GetComponent<GameManager>().OnGameOver();
        GameManager.Instance.OnGameOver();
    }    
}
