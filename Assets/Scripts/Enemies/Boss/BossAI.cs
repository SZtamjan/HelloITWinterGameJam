using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] private EnemyStruct enemyStats;
    private int maxHP;
    [SerializeField] private float maxShootSpeed = 1f;
    [SerializeField] private float keepDistanceFromPlayer = 1f;
    
    [Header("Boss Stages V2")] 
    [SerializeField] private List<BossStage> stages = new List<BossStage>();

    private float currentShootSpeed;
    
    private Transform playerPos;

    void Start()
    {
        Initialize();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, playerPos.position);
        if (distance > keepDistanceFromPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, enemyStats.moveSpeed * Time.deltaTime);
        }
    }

    private void Initialize()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        currentShootSpeed = enemyStats.shootSpeed;
        maxHP = enemyStats.hp;
    }

    public void GetDmg(int dmg)
    {
        enemyStats.hp -= dmg;
        Debug.Log("My boss HP: " + enemyStats.hp);
        
        //Die if dead
        if (enemyStats.hp <= 0)
        {
            Die();
        }

        //shoot faster
        float hpPercent = (enemyStats.hp / maxHP) * 100f;
        float newShootingSpeed = ((enemyStats.shootSpeed - maxShootSpeed) / 100f * hpPercent + maxShootSpeed);
        Debug.Log("Wartosc " + newShootingSpeed);
        currentShootSpeed = newShootingSpeed;

        //bossStage
        // int AmountOfStages = stages.Count - 1;
        // if (AmountOfStages >= 0)
        // {
        //     for (int i = AmountOfStages; i > 0; i--)
        //     {
        //         
        //     }
        // }
        // TO IMPROVE LATER
        
        
        if (enemyStats.hp < stages[2].activatesAt)
        {
            BossStageThree();
        }else if (enemyStats.hp < stages[1].activatesAt)
        {
            BossStageTwo();
        }else if (enemyStats.hp < stages[0].activatesAt)
        {
            BossStageOne();
        }
    }

    private void BossStageOne()
    {
        //Add one more turret
    }
    
    private void BossStageTwo()
    {
        //Adds Super Umiejetnosc
    }
    
    private void BossStageThree()
    {
        maxShootSpeed -= stages[2].shootsFaster;
        currentShootSpeed -= stages[2].shootsFaster;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}