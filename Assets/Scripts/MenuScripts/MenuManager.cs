using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private float currentCheck = 0;
    [SerializeField] private List<GameObject> buttons = new List<GameObject>();

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
            Debug.Log(currentCheck);
            
            PlayAnim();
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
                    Debug.Log("co to extras");
                    break;
                case 3:
                    ExitGame();
                    break;
            }
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
        
    }

    private void ExitGame()
    {
        
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
