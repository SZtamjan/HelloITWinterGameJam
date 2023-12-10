using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float maxDistance = 15f;

    [SerializeField] private GameObject leftClamp;
    [SerializeField] private GameObject rightClamp;
    private bool isInSS = false;

    private Vector3 startPos = new Vector3();

    private void Start()
    {
        startPos = transform.position;
        isInSS = GameManager.Instance.GetIsInSpaceShooter;
    }
    
    private void Update()
    {
        float dist = Vector2.Distance(startPos, transform.position);
        if (dist > maxDistance)
        {
            KillMe();
        }
        
        Debug.Log("bullet read: " + isInSS);
        if (isInSS)
        {
            if (transform.position.x < leftClamp.transform.position.x+0.2f)
            {
                KillMe();
            }

            if (transform.position.x > rightClamp.transform.position.x-0.2f)
            {
                KillMe();
            }
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
