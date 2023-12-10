using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundHandler : MonoBehaviour
{
    [SerializeField] private GameObject background;

    [SerializeField] private GameObject bgdzien;
    [SerializeField] private GameObject bgnoc;
    private GameObject currentBG;

    [SerializeField] private Vector2 spawnPos = new Vector2();
    [SerializeField] private float spawnTimingOne = 4.55f;
    [SerializeField] private float spawnTimingTwo = 5f;
    private float currentSpawnTiming = 1f;
    
    //OUT OF USE
    private Quaternion startPos = new Quaternion();
    private Vector3 finalPos = new Vector3(0, 0, 0);
    //^^^^^^^^^^^^^^^^^^^
    
    private Coroutine spaceShooterBackground;
    private Coroutine bulletBackground;

    private void Start()
    {
        
        //StartCoroutine(VerticalMove());
    }

    public void StartVerticalMove()
    {
        currentSpawnTiming = spawnTimingOne;
        currentBG = bgdzien;
        spaceShooterBackground = StartCoroutine(VerticalMove());
    }

    public IEnumerator VerticalMove()
    {
        float time = 0f;
        while (true)
        {
            if (time > currentSpawnTiming)
            {
                Instantiate(currentBG, spawnPos, Quaternion.identity);
                time = 0f;
            }

            time += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    public void SwitchBGToBullet()
    {
        currentSpawnTiming = spawnTimingTwo;
        currentBG = bgnoc;
        spawnPos = new Vector2(spawnPos.x, spawnPos.y + 4.67462f);

        StartCoroutine(WaitForBackGroundThenLoad());
    }

    private IEnumerator WaitForBackGroundThenLoad()
    {
        yield return new WaitForSeconds(6f);
        GetComponent<GameManager>().ChangeStateTo(GameState.LoadNextWave);
    }

    public IEnumerator RotateBackground() //najpierw zrób corutyne ktora zatrzymuje bg w odpowiednim miejscu a potem niech przejdzie w tą
    {
        startPos = background.transform.rotation;
        Quaternion qFinalPos = Quaternion.Euler(finalPos.x,finalPos.y,finalPos.z);
        yield return new WaitForSeconds(2f);
        
        while (background.transform.rotation != qFinalPos)
        {
            background.transform.rotation = Quaternion.Lerp(background.transform.rotation,qFinalPos,1f*Time.deltaTime);
            yield return null;
        }
        
        GetComponent<GameManager>().ChangeStateTo(GameState.LoadNextWave);
    }
}
