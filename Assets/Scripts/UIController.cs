using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    
    [SerializeField] private TextMeshProUGUI scoreTMPro;


    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScore(int value)
    {
        scoreTMPro.text = "Score: " + value.ToString();
    }

}
