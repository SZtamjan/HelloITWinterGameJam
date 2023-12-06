using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
                ChangeStateTo(GameState.StartGame);
                break;
            case GameState.StartGame:
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

    #region StartGame

    private void StartSpawningEnemies()
    {
        StartCoroutine(GetComponent<EnemySpawner>().SpawnEnemies());
    }

    #endregion

    private void ChangeStateTo(GameState newState)
    {
        ChangeGameState(newState);
    }
    
}

public enum GameState
{
    Initialization,
    StartGame,
    EndGame
}