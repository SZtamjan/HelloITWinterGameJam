using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private GameObject inGameMenu;
    private bool inGameMenuOn = false;
    
    private float currentCheck = 0;
    [SerializeField] private List<GameObject> buttons = new List<GameObject>();
    
    
    [SerializeField] private TextMeshProUGUI scoreTMPro;
    [SerializeField] private TextMeshProUGUI distanceTMPro;
    [SerializeField] private TextMeshProUGUI winScreenPoints;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScore(int value)
    {
        scoreTMPro.text = "Score: " + value.ToString();
    }

    public void UpdateDistance(int value)
    {
        distanceTMPro.text = "Distance left " + value;
    }

    public void TurnOffDistanceLeft()
    {
        distanceTMPro.gameObject.SetActive(false);
    }
    
    public void UpdateWinScreenPoints(int value)
    {
        winScreenPoints.text = "Your points " + value;
    }
    
    public void ShowInGameMenu()
    {
        inGameMenu.SetActive(true);
        inGameMenuOn = true;

        ClearAnims();
        PlayAnim();
    }

    private void Update()
    {
        if (inGameMenuOn)
        {
            if (Input.GetButtonDown("Vertical"))
            {
                float dwa = Input.GetAxisRaw("Vertical");
                currentCheck -= dwa;
                currentCheck = Mathf.Clamp(currentCheck,0,1);
                Debug.Log(currentCheck);
            
                PlayAnim();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                switch (currentCheck)
                {
                    case 0:
                        TryAgain();
                        break;
                    case 1:
                        BackToMenu();
                        break;
                }
            }
        }
    }

    private void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void BackToMenu()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        index--;
        SceneManager.LoadScene(index);
    }
    
    private void PlayAnim()
    {
        ClearAnims();
        
        buttons[(int)currentCheck].transform.GetChild(0).gameObject.SetActive(true);
    }

    private void ClearAnims()
    {
        buttons[0].transform.GetChild(0).gameObject.SetActive(false);
        buttons[1].transform.GetChild(0).gameObject.SetActive(false);
    }
    
}
