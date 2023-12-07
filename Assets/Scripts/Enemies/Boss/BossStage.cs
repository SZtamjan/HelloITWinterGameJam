using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BossStage
{
    public int activatesAt;
    public float shootsFaster;
    [Tooltip("DO NOT TOUCH")] public bool activated;
}