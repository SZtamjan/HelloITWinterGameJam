using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    //Enemies
    [Header("Enemies")]
    [SerializeField] private GameObject enemyPrefab;
    
    //Destanations
    [Header("Destanations")]
    [SerializeField] private Transform destanationsTransform;
    private List<Transform> destanations = new List<Transform>();
    
    //Waves
    [Header("Waves")] 
    [Tooltip("In seconds")] [SerializeField] private float breakBetweenWaves = 1f;
    [SerializeField] private int amountOfEnemies;
    [SerializeField] private int spawnCooldown;
    
    [Tooltip("It can be easly implemented if needed")] 
    [SerializeField] private int amountOfEnemiesPerOneSpawn = 1;

    //Screen Bounds
    private Vector2 minBounds = new Vector2();
    private Vector2 maxBounds = new Vector2();
    
    //Spawn Area
    private float spawnHeight = 0f;
    private Vector2 spawnHorizontalBounds = new Vector2();


    #region Initialization

    public void Initialize()
    {
        InitScreenBounds();
        InitSpawnArea();
        SetDestanations();
    }

    private void InitScreenBounds()
    {
        minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void InitSpawnArea()
    {
        spawnHeight = maxBounds.y + 1f;
        spawnHorizontalBounds = new Vector2(minBounds.x, maxBounds.x);
    }
    
    private void SetDestanations() //This solution is format sensitive
    {
        foreach (Transform child in destanationsTransform)
        {
            destanations.Add(child);
        }
    }

    #endregion

    #region OnStartGame
    
    public IEnumerator SpawnEnemies()
    {
        int spawnedAmountOfEnemies = 0;
        while (amountOfEnemies > spawnedAmountOfEnemies)
        {
            Debug.Log(amountOfEnemies + " and " +spawnedAmountOfEnemies);
            for (int i = 0; i < amountOfEnemiesPerOneSpawn; i++) //Not really implemented (It spawns x amount of enemies at once)
            {
                Vector2 newSpawnPoint = new Vector2(Random.Range(spawnHorizontalBounds.x, spawnHorizontalBounds.y),spawnHeight);
                Transform newDestanation = destanations[Random.Range(0,destanations.Count)];
                
                GameObject newEnemy = Instantiate(enemyPrefab, newSpawnPoint, Quaternion.identity);
                newEnemy.GetComponent<EnemyMover>().PopulateDestanationAndGo(newDestanation.position);
            }

            spawnedAmountOfEnemies++;
            yield return new WaitForSeconds(spawnCooldown);
        }

        yield return new WaitForSeconds(breakBetweenWaves);
        ChangeGameState();
        
        yield return null;
    }

    #endregion

    private void ChangeGameState()
    {
        GetComponent<GameManager>().ChangeStateTo(GameState.LoadNextWave);
    }
    
    public void LoadWave(WaveStruct newWaveInfo)
    {
        this.amountOfEnemies = newWaveInfo.amountOfEnemies;
        this.spawnCooldown = newWaveInfo.spawnCooldown;
    }
    
}
