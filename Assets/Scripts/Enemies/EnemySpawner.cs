using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    
    //Screen Bounds
    private Vector2 minBounds = new Vector2();
    private Vector2 maxBounds = new Vector2();
    
    //Spawn Area
    private float spawnHeight = 0f;
    private Vector2 spawnHorizontalBounds = new Vector2();

    [SerializeField] private int amountOfEnemiesPerOneSpawn = 1;
    
    
    #region Initialization

    public void Initialize()
    {
        InitScreenBounds();
        InitSpawnArea();
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

    #endregion

    #region OnStartGame

    public IEnumerator SpawnEnemies()
    {
        while (true)
        {
            for (int i = 0; i < amountOfEnemiesPerOneSpawn; i++)
            {
                Vector2 newSpawnPoint = new Vector2(Random.Range(spawnHorizontalBounds.x, spawnHorizontalBounds.y),spawnHeight);
                GameObject newEnemy = Instantiate(enemyPrefab, newSpawnPoint, Quaternion.identity);
            }
            
            yield return new WaitForSeconds(0.5f);
        }
        
        yield return null;
    }

    #endregion
    
}
