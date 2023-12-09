using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OneShotingBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5f;
    
    void Update()
    {
        Vector2 moveTo = transform.position + Vector3.down;
        transform.position = Vector2.MoveTowards(transform.position, moveTo, bulletSpeed * Time.deltaTime);
        if (transform.position.y < -10f)
        {
            KillMe();
        }
    }


    private void KillMe()
    {
        Destroy(gameObject);
    }
}
