using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveOjbDown : MonoBehaviour
{
    [SerializeField] private float objMoveSpeed = 5f;
    
    void Update()
    {
        Vector2 moveTo = transform.position + Vector3.down;
        transform.position = Vector2.MoveTowards(transform.position, moveTo, objMoveSpeed * Time.deltaTime);
        if (transform.position.y < -10f)
        {
            KillMe();
        }
    }


    public void KillMe()
    {
        Destroy(gameObject);
    }
}
