using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    
    private float currentCheck = 0;
    [SerializeField] private List<GameObject> buttons = new List<GameObject>();

    private bool allowChangeMusic = false;
    
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject extras;
    
    private void Start()
    {
        ClearAnims();
        PlayAnim();
    }

    private void Update()
    {
        
        if (Input.GetButtonDown("Vertical"))
        {
            float dwa = Input.GetAxisRaw("Vertical");
            currentCheck -= dwa;
            currentCheck = Mathf.Clamp(currentCheck,0,3);
            
            PlayAnim();
            Debug.Log(currentCheck);
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (currentCheck)
            {
                case 0:
                    NewGame();
                    break;
                case 1:
                    OpenSettings();
                    break;
                case 2:
                    OpenExtras();
                    Debug.Log("co to extras");
                    break;
                case 3:
                    ExitGame();
                    break;
            }
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            float dwa = Input.GetAxisRaw("Vertical");
            if (dwa > 0) _audioManager.ChangeSliderValue(+0.1f);
            if (dwa < 0) _audioManager.ChangeSliderValue(-0.1f);
        }
        
    }
    
    private void NewGame()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        index++;
        SceneManager.LoadScene(index);
    }

    private void OpenSettings()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }

    private void OpenExtras()
    {
        mainMenu.SetActive(false);
        extras.SetActive(true);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void BackToMenu()
    {
        //wylacza:
        settings.SetActive(false);
        extras.SetActive(false);
        
        //wlacza:
        mainMenu.SetActive(true);
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
        buttons[2].transform.GetChild(0).gameObject.SetActive(false);
        buttons[3].transform.GetChild(0).gameObject.SetActive(false);
    }
    
}
