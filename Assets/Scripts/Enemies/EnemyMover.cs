using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform destanationsTransform;

    private List<Transform> destanations = new List<Transform>();

    private void Start()
    {
        Initialization();
    }

    





    private void Initialization()
    {
        SetDestanations();
    }
    
    private void SetDestanations() //This solution is format sensitive
    {
        foreach (Transform child in destanationsTransform)
        {
            destanations.Add(child);
        }
    }
    
}
