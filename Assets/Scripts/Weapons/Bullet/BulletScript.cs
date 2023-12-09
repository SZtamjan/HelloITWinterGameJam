using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float maxDistance = 5f;

    private Vector3 startPos = new Vector3();

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float dist = Vector2.Distance(startPos, transform.position);
        if (dist > maxDistance)
        {
            KillMe();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyMover>().EnemyDie();
            GameManager.Instance.AddPoints(10);
            KillMe();
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<BossAI>().GetDmg(1);
            KillMe();
        }
        
        
    }


    private void KillMe()
    {
        Destroy(gameObject);
    }
    
}
