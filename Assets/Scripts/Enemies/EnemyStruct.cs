using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct EnemyStruct
{
    public int hp;
    public float moveSpeed;
    [Tooltip("Cooldown actually")] public float startingShootSpeed;
}