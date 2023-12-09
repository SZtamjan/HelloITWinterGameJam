using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct BossStage
{
    public float activatesAtHP;
    public float shortenShootSpeedBy;
    [Tooltip("DO NOT TOUCH")] public bool activated;
}