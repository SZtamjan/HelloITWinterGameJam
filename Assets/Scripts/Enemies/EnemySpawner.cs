using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    //Enemies
    [Header("Enemies")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bossPrefab;
    
    //Destanations
    [Header("Destanations")]
    [SerializeField] private Transform destanationsTransform;
    private List<Transform> destanations = new List<Transform>();
    
    //Waves
    [Header("Waves")] 
    [Tooltip("In seconds")] [SerializeField] private float breakBetweenWaves = 1f;
    
    [Header("")]
    [Header("Shown for Debug purposes")]
    [SerializeField] private int amountOfEnemies;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private bool bossWave;
    
    [Tooltip("It can be easly implemented if needed")] 
    [SerializeField] private int amountOfEnemiesPerOneSpawn = 1;

    //Screen Bounds
    private Vector2 minBounds = new Vector2();
    private Vector2 maxBounds = new Vector2();
    
    //Spawn Area
    private float spawnHeight = 0f;
    private Vector2 spawnHorizontalBounds = new Vector2();

    public bool DBG = false;

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
        //vars initialization
        int spawnedAmountOfEnemies = 0;
        int amountOfEnemiesLeft = 0;
        int amountOfBossesLeft = 0;
        
        //logic
        
        //spawn enemies
        while (amountOfEnemies > spawnedAmountOfEnemies)
        {
            //Debug.Log(amountOfEnemies + " and " +spawnedAmountOfEnemies);
            SpawnEnemy();

            spawnedAmountOfEnemies++;
            yield return new WaitForSeconds(spawnCooldown);
        }
        
        //spawn boss
        if(bossWave) SpawnBoss();

        //Hold wave until enemies die
        do
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            amountOfEnemiesLeft = enemies.Length;
            //Debug.Log("Ilosc enemy: " + objs.Length);

            yield return new WaitForSeconds(0.5f);

        }while(amountOfEnemiesLeft != 0);
        
        Debug.Log("XDDDD");
        
        yield return new WaitForSeconds(breakBetweenWaves);
        ChangeGameState();
        
        yield return null;
    }

    private void SpawnBoss()
    {
        Vector2 newSpawnPoint = CalculateSpawnPosition();
        Instantiate(bossPrefab, newSpawnPoint, Quaternion.identity);
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < amountOfEnemiesPerOneSpawn; i++) //Not really implemented (It spawns x amount of enemies at once)
        {
            Vector2 newSpawnPoint = CalculateSpawnPosition();
            Transform newDestanation = destanations[Random.Range(0,destanations.Count)];
                
            GameObject newEnemy = Instantiate(enemyPrefab, newSpawnPoint, Quaternion.identity);
            newEnemy.GetComponent<EnemyMover>().PopulateDestanationAndGo(newDestanation.position);
        }
    }

    private Vector2 CalculateSpawnPosition()
    {
        return new Vector2(Random.Range(spawnHorizontalBounds.x, spawnHorizontalBounds.y),spawnHeight);
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
        this.bossWave = newWaveInfo.bossWave;
    }
    
}
