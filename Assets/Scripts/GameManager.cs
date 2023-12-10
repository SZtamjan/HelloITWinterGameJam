using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    //Vars
    private GameState currentlyInState;
    private GameObject _playerObj;
    private UIController _uiController;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameEvent deathEvent;
    
    [Header("SpaceShooter part")]
    [SerializeField] private int endSpaceShooterAfterRoad = 10;
    
    //dynamic vars
    private bool spaceShooterMode = true;
    private bool upgradeOne, upgradeTwo, upgradeThree;
    
    //properties
    public int pointsProperty
    {
        get
        {
            return points;
        }
        private set
        {
            points = value;
            
            if (points >= unLockThreeAt && !upgradeThree)
            {
                _playerObj.GetComponent<PlayerScript>().UpgradeLvlProp = 3;
                upgradeThree = true;
            }else if (points >= unLockSecondAt && !upgradeTwo)
            {
                _playerObj.GetComponent<PlayerScript>().UpgradeLvlProp = 2;
                upgradeTwo = true;
            }else if (points >= unLockFirstAt && !upgradeOne)
            {
                _playerObj.GetComponent<PlayerScript>().UpgradeLvlProp = 1;
                upgradeOne = true;
            }
        }
    }

    public bool GetIsInSpaceShooter
    {
        get
        {
            return spaceShooterMode;
        }
    }

    [Header("Player Upgrades")] 
    [SerializeField] private int unLockFirstAt = 0;
    [SerializeField] private int unLockSecondAt = 0;
    [SerializeField] private int unLockThreeAt = 0;
    
    //Score
    private int points;
    
    
    
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
                SetUpUI();
                //ChangeStateTo(GameState.LoadNextWave);
                ChangeStateTo(GameState.SpaceShooter);
                break;
            case GameState.SpaceShooter:
                SpaceShooterStartSpawningEnemies();
                StartBackground();
                CheckIfRoadComplete();
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
            case GameState.EndGameLose:
                StopEverything();
                ShowDeathScreen();
                break;
            case GameState.EndGameWin:
                StopEverything();
                ShowWinScreen();
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
    private void SetUpUI()
    {
        _uiController.UpdateScore(points);
        _uiController.UpdateDistance(endSpaceShooterAfterRoad);
    }

    #endregion

    #region StartSpaceShooter

    private void SpaceShooterStartSpawningEnemies()
    {
        StartCoroutine(GetComponent<EnemySpawner>().SpaceShooterStartSpawnEnemies());
    }

    private void CheckIfRoadComplete()
    {
        StartCoroutine(SSRoad());
    }

    private IEnumerator SSRoad()
    {
        while (spaceShooterMode)
        {
            endSpaceShooterAfterRoad -= 2;
            _uiController.UpdateDistance(endSpaceShooterAfterRoad);
            yield return new WaitForSeconds(1f);

            if (endSpaceShooterAfterRoad <= 0)
            {
                spaceShooterMode = false;
                ChangeStateTo(GameState.ChangeGameType);
                _uiController.TurnOffDistanceLeft();
            }
        }
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
        //StartCoroutine(GetComponent<BackGroundHandler>().RotateBackground());
        GetComponent<BackGroundHandler>().SwitchBGToBullet();
    }

    private void StartBackground()
    {
        GetComponent<BackGroundHandler>().StartVerticalMove();
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
        //StartCoroutine(GetComponent<EnemySpawner>().SpawnEnemies());
        GetComponent<EnemySpawner>().StartEnemySpawn();
    }

    #endregion

    #region EndGame

    private void ShowWinScreen()
    {
        winScreen.SetActive(true);
        _uiController.UpdateWinScreenPoints(points);
    }
    
    public void ShowDeathScreen()
    {
        _uiController.ShowInGameMenu();
    }

    #endregion
    
    private void StopEverything()
    {
        deathEvent.Raise();
    }
    
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
        if (currentlyInState == GameState.EndGameWin && newState == GameState.EndGameLose)
        {
            
        }else if (currentlyInState == GameState.EndGameLose && newState == GameState.EndGameWin)
        {
            
        }
        else
        {
            ChangeGameState(newState);
            currentlyInState = newState;
        }
        
    }
    
}

public enum GameState
{
    Initialization,
    SpaceShooter,
    ChangeGameType,
    LoadNextWave,
    StartWave,
    EndGameLose,
    EndGameWin
}