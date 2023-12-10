using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private bool betterEnemy;
    [SerializeField] private EnemyStruct enemyStats;
    [SerializeField] private GameObject heal;
    
    private Vector3 destanation = new Vector3();
    [SerializeField] private Transform destanationOne;
    [SerializeField] private Transform destanationTwo;
    private Vector3 goThere = new Vector3();
    private bool XD = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("WESZEDLEM W PLAYERA");
            other.gameObject.GetComponent<PlayerScript>().GetOneDmg();
            EnemyDie();
        }
    }

    #region EnemyMovement

    public void PopulateDestanationAndGo(Vector2 newDestanation)
    {
        if (betterEnemy)
        {
            destanation = newDestanation;
            goThere = destanationOne.position;
            enemyStats.moveSpeed = enemyStats.moveSpeed * 6f;
            StartCoroutine(MoveTowardsDestanation());
        }
        else
        {
            destanation = newDestanation;
            StartCoroutine(MoveTowardsDestanation());
        }
        
    }

    private IEnumerator MoveTowardsDestanation()
    {
        float czas = 0f;
        while (transform.position != destanation)
        {
            if (betterEnemy)
            {
                // if (XD)
                // {
                //     //goThere = destanationOne.transform.position;
                //     goThere = Vector3.Lerp(goThere, destanationOne.transform.position, 1f*Time.deltaTime);
                // }
                // else
                // {
                //     //goThere = destanationTwo.transform.position;
                //     goThere = Vector3.Lerp(goThere, destanationTwo.transform.position, 1f*Time.deltaTime);
                // }
                //
                // if (czas > 2f)
                // {
                //     if (XD)
                //     {
                //         XD = false;
                //     }
                //     else
                //     {
                //         XD = true;
                //     }
                //
                //     czas = 0f;
                // }
                
                goThere = destanationOne.position;
                transform.position = Vector2.MoveTowards(transform.position,goThere,enemyStats.moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position,destanation,enemyStats.moveSpeed * Time.deltaTime);
                if(transform.position.y < - 10f) EnemyDie();
            }

            czas += Time.deltaTime;
            yield return null;
        }

        EnemyDie();
    }

    #endregion

    public void EnemyDie()
    {
        int chance = Random.Range(0, 100);
        if (chance > 80) //20% chance for heal
        {
            Instantiate(heal, transform.position, Quaternion.identity);
        }        
        
        //ultra giga efekty i wgl xd
        Destroy(gameObject);
    }
}
