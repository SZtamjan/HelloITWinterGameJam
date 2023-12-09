using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private EnemyStruct enemyStats;
    private Vector3 destanation = new Vector3();

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("WESZEDLEM W PLAYERA");
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerScript>().GetOneDmg();
            EnemyDie();
        }
    }

    #region EnemyMovement

    public void PopulateDestanationAndGo(Vector3 newDestanation)
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
        //ultra giga efekty i wgl xd
        Destroy(gameObject);
    }
}
