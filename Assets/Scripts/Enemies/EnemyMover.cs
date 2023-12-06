using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 1f;
    private Vector3 destanation = new Vector3();
    
    public void PopulateDestanationAndGo(Vector3 newDestanation)
    {
        destanation = newDestanation;
        StartCoroutine(MoveTowardsDestanation());
    }

    private IEnumerator MoveTowardsDestanation()
    {
        while (transform.position != destanation)
        {
            transform.position = Vector3.MoveTowards(transform.position,destanation,enemySpeed * Time.deltaTime);
            yield return null;
        }

        EnemyDie();
    }

    private void EnemyDie()
    {
        //ultra giga efekty i wgl xd
        Destroy(gameObject);
    }
}
