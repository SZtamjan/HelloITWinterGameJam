using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBGDown : MonoBehaviour
{
    [SerializeField] private Transform here;
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, here.position, 2f * Time.deltaTime);
        if (transform.position.y < -20f)
        {
            Destroy(gameObject);
        }
    }
}
