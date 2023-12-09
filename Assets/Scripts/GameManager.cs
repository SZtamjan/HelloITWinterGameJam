using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    //Vars
    private GameObject _playerObj;
    private UIController _uiController;

    [Header("SpaceShooter part")]
    [SerializeField] private int endSpaceShooterAtPoints = 10;
    
    //dynamic vars
    private bool spaceShooterMode = true;
    
    //Score
    private int points;
    public int pointsProperty
    {
        get
        {
            return points;
        }
        private set
        {
            points = value;
            if (points >= endSpaceShooterAtPoints && spaceShooterMode)
            {
                ChangeStateTo(GameState.ChangeGameType);
                
                spaceShooterMode = false;
            }
        }
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
                //ChangeStateTo(GameState.LoadNextWave);
                ChangeStateTo(GameState.SpaceShooter);
                break;
            case GameState.SpaceShooter:
                SpaceShooterStartSpawningEnemies();
                break;
            case GameState.ChangeGameType:
                ChangePlayerMovement();
                ChangeSpawnBehavior();
                ChangeBackground(); //When this one is done it activates LoadNextWave
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
        _playerObj = GameObject.FindGameObjectWithTag("Player");
        _uiController = UIController.Instance;
    }
    
    //Score
    private void InitScore()
    {
        pointsProperty = 0;
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

    #region StartSpaceShooter

    private void SpaceShooterStartSpawningEnemies()
    {
        StartCoroutine(GetComponent<EnemySpawner>().SpaceShooterStartSpawnEnemies());
    }

    #endregion

    #region ChangeGameType

    private void ChangePlayerMovement()
    {
        _playerObj.GetComponent<PlayerMover>().spaceShooterMovement = false;
    }

    private void ChangeSpawnBehavior()
    {
        GetComponent<EnemySpawner>().duringSpaceShooter = false;
    }

    private void ChangeBackground()
    {
        StartCoroutine(GetComponent<BackGroundHandler>().RotateBackground());
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
        pointsProperty++;
    }

    public void AddPoints(int value)
    {
        pointsProperty += value;
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
    SpaceShooter,
    ChangeGameType,
    LoadNextWave,
    StartWave,
    EndGame
}