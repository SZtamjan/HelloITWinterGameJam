using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Waves
    [Header("Waves")] 
    [SerializeField] private List<WaveStruct> waves;
    private int waveToLoad = 0;
    
    
    private void Start()
    {
        ChangeGameState(GameState.Initialization);
    }

    private void ChangeGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Initialization:
                InitializeSpawn();
                ChangeStateTo(GameState.LoadNextWave);
                break;
            case GameState.LoadNextWave:
                CheckIfThereIsNextWave();
                break;
            case GameState.StartWave:
                 StartSpawningEnemies();
                 break;
        }
    }

    

    #region Initialize

    //Spawn Script
    private void InitializeSpawn()
    {
        GetComponent<EnemySpawner>().Initialize();
    }

    #endregion

    #region LoadWave

    private void CheckIfThereIsNextWave()
    {
        if (waveToLoad + 1 > waves.Count)
        {
            Debug.Log("Ran out of waves");
        }
        else
        {
            LoadNextWave();
        }
    }
    
    private void LoadNextWave()
    {
        GetComponent<EnemySpawner>().LoadWave(waves[waveToLoad]);
        Debug.Log("Loaded " + waveToLoad);
        waveToLoad++;
        
        ChangeStateTo(GameState.StartWave);
    }

    #endregion
    
    #region StartWave

    private void StartSpawningEnemies()
    {
        StartCoroutine(GetComponent<EnemySpawner>().SpawnEnemies());
    }

    #endregion

    public void ChangeStateTo(GameState newState)
    {
        ChangeGameState(newState);
    }
    
}

public enum GameState
{
    Initialization,
    LoadNextWave,
    StartWave,
    EndGame
}