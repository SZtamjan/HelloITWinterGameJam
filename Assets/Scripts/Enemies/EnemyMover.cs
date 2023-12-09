using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private EnemyStruct enemyStats;
    [SerializeField] private GameObject heal;
    
    private Vector3 destanation = new Vector3();

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
        destanation = newDestanation;
        StartCoroutine(MoveTowardsDestanation());
    }

    private IEnumerator MoveTowardsDestanation()
    {
        while (transform.position != destanation)
        {
            transform.position = Vector2.MoveTowards(transform.position,destanation,enemyStats.moveSpeed * Time.deltaTime);
            if(transform.position.y < - 10f) EnemyDie();
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
