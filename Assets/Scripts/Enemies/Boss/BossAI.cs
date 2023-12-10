using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BossAI : MonoBehaviour
{
    [SerializeField] private EnemyStruct bossStats;
    
    private int maxHP;

    [Header("Weapons & Guns")] 
    [SerializeField] private GameObject snowBall;
    [SerializeField] private GameObject candy;
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject shootingSpotMid;
    [SerializeField] private GameObject shootingSpotLeft;
    [SerializeField] private GameObject shootingSpotRight;

    [Header("Boss specials")] [SerializeField]
    private GameObject heal;
    [Tooltip("Cooldown actually")] [SerializeField] private float maxShootSpeed = 1f;

    private Vector2 leftPoint = new Vector2();
    private Vector2 rightPoint = new Vector2();
    private bool movingLeft = true;
    private bool forceStop = false;
    private float xd = 0f;
    private float lol = 0f;
    
    [Header("Boss Stages")] 
    [Tooltip("Designed specifically for 3!!!")]
    [SerializeField] private List<BossStage> stages = new List<BossStage>();
    private bool one, two, three;

    private float currentShootSpeed;

    void Start()
    {
        Initialize();
        StartCoroutine(StartShooting());
        StartCoroutine(StartDroppingHeals());
    }

    private IEnumerator StartDroppingHeals()
    {
        Vector2 pos = transform.position;
        pos.y = pos.y + 4f;
        while (true)
        {
            Instantiate(heal, pos, Quaternion.identity);
            yield return new WaitForSeconds(10f);
        }
    }
    
    private void Update()
    {
        xd += Time.deltaTime;
        lol += Time.deltaTime;
        if (!forceStop)
        {
            if (movingLeft)
            {
                transform.position = Vector2.MoveTowards(transform.position, leftPoint, bossStats.moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, leftPoint) < 0.2f)
                {
                    movingLeft = false;
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, rightPoint, bossStats.moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, rightPoint) < 0.2f)
                {
                    movingLeft = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        //Vector3 dir = playerPos.position - transform.position;
        //transform.up = dir;
    }

    private void Initialize()
    {
        leftPoint = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.85f));
        rightPoint = Camera.main.ViewportToWorldPoint(new Vector2(1, 0.85f));
        
        currentShootSpeed = bossStats.startingShootSpeed;
        maxHP = bossStats.hp;
    }


    private Coroutine dwadwa;
    private IEnumerator StartShooting()
    {
        while (true)
        {
            if (one)
            {
                if (dwadwa == null) dwadwa = StartCoroutine(BasicGo());
                yield return null;
            }else if (two)
            {
                Debug.Log("XDDDXDXDXDXD" + xd);
                if (xd > 2f)
                {
                    Instantiate(candy, shootingSpotMid.transform.position, Quaternion.identity);
                    xd = 0f;
                }
                yield return null;
                
            }else if (three)
            {
                if (xd > 3.5f)
                {
                    Instantiate(candy, shootingSpotMid.transform.position, Quaternion.identity);
                    xd = 0f;
                }
                //lasery i wgl
                if (lol > 0.8f)
                {
                    forceStop = true;
                    laser.SetActive(true);
                    yield return new WaitForSeconds(0.2f);
                    forceStop = false;
                    laser.SetActive(false);
                    lol = 0f;
                }
                yield return null;
                
            }
            else //default
            {
                Instantiate(snowBall, shootingSpotMid.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(currentShootSpeed);
            }

            yield return null;
        }
    }

    private IEnumerator BasicGo()
    {
        while (true)
        {
            Instantiate(snowBall, shootingSpotLeft.transform.position, Quaternion.identity);
            Instantiate(snowBall, shootingSpotRight.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(currentShootSpeed);
        }
    }

    public void GetDmg(int dmg)
    {
        bossStats.hp -= dmg;

        //Die if dead
        if (bossStats.hp <= 0)
        {
            Die();
        }

        //shoot faster
        float hpPercent = ((float)bossStats.hp / maxHP) * 100f;
        Debug.Log("procentHP bosa: " + hpPercent);
        float newShootingSpeed = ((bossStats.startingShootSpeed - maxShootSpeed) / 100f * hpPercent + maxShootSpeed);
        currentShootSpeed = newShootingSpeed;

        //Boss Stages
        if ((hpPercent < stages[2].activatesAtHP+1f))
        {
            BossStageThree();
        }else if ((hpPercent < stages[1].activatesAtHP+1f))
        {
            BossStageTwo();
        }else if ((hpPercent < stages[0].activatesAtHP+1f))
        {
            BossStageOne();
        }
    }

    private void BossStageOne()
    {
        //Add one more turret
        Debug.Log("Stage one activated");
        one = true;
        two = false;
        three = false;
    }
    
    private void BossStageTwo()
    {
        //Adds Super Umiejetnosc
        Debug.Log("Stage two activated");
        one = false;
        two = true;
        three = false;
    }
    
    private void BossStageThree()
    {
        //laser
        maxShootSpeed -= stages[2].shortenShootSpeedBy;
        currentShootSpeed -= stages[2].shortenShootSpeedBy;
        one = false;
        two = false;
        three = true;
    }

    private void Die()
    {
        GameManager.Instance.AddPoints(1000);
        GameManager.Instance.ChangeStateTo(GameState.EndGameWin);
        Destroy(gameObject);
    }
}