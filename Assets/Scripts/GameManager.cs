using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    //Vars
    private UIController _uiController;
    
    //Score
    public int points
    {
        get;
        private set;
    }
    
    
    //Waves
    [Header("Waves")] 
    [SerializeField] private List<WaveStruct> waves;
    private int waveToLoad = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeGameState(GameState.Initialization);
    }

    private void ChangeGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Initialization:
                InitVars();
                InitializeSpawn();
                InitScore();
                SetUpScore();
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

    //Vars
    private void InitVars()
    {
        _uiController = UIController.Instance;
    }
    
    //Score
    private void InitScore()
    {
        points = 0;
    }
    
    //Spawn Script
    private void InitializeSpawn()
    {
        GetComponent<EnemySpawner>().Initialize();
    }
    
    //Set ups
    private void SetUpScore()
    {
        _uiController.UpdateScore(points);
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

    public void AddPoint()
    {
        points++;
    }

    public void AddPoints(int value)
    {
        points += value;
        _uiController.UpdateScore(points);
    }

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