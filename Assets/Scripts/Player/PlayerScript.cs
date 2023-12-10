using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject pointer;

    [SerializeField] private int hp = 3;
    [SerializeField] private List<GameObject> RenifersSpots;
    [SerializeField] private GameObject reindeer;

    //Shooting
    [Header("Shooting")] 
    [SerializeField] private GameObject bulletSpotOne;
    [SerializeField] private GameObject bulletSpotTwo;
    [SerializeField] private GameObject bulletSpotThree;
    private Coroutine shootCor;
    private float czas = 0f;
    [SerializeField] private float playerGunCoolDown = 1f;
    private float basePlayerGunCoolDown = 1f;
    private int upgradeLvl = 0; //0 is default
    private bool cooldownCut = false;

    public int UpgradeLvlProp
    {
        set
        {
            upgradeLvl = value;
        }
    }

    private void Start()
    {
        basePlayerGunCoolDown = playerGunCoolDown;
    }

    private void Update()
    {
        czas += Time.deltaTime;
        if (Input.GetMouseButton(0) && czas > playerGunCoolDown)
        {
            if (upgradeLvl == 0)
            {
                Debug.Log("Upgrade default");
                playerGunCoolDown = basePlayerGunCoolDown;
                ShootFromSpotOne();
            }
            else if (upgradeLvl == 1)
            {
                Debug.Log("Upgrade 1");
                playerGunCoolDown = basePlayerGunCoolDown;
                ShootFromSpotOne();
                ShootFromSpotTwo();
            }
            else if (upgradeLvl == 2)
            {
                Debug.Log("Upgrade 2");
                playerGunCoolDown = basePlayerGunCoolDown;
                ShootFromSpotOne();
                ShootFromSpotTwo();
                ShootFromSpotThree();
            }
            else if (upgradeLvl == 3)
            {
                playerGunCoolDown = basePlayerGunCoolDown / 1.5f;

                ShootFromSpotOne();
                ShootFromSpotTwo();
                ShootFromSpotThree();
            }
            
            czas = 0f;
        }
    }

    private void ShootFromSpotOne()
    {
        GameObject blt = Instantiate(bullet, bulletSpotOne.transform.position, transform.rotation);
        blt.GetComponent<Rigidbody2D>().AddForce(transform.up * 50f,ForceMode2D.Force);
    }

    private void ShootFromSpotTwo()
    {
        GameObject blt = Instantiate(bullet, bulletSpotTwo.transform.position, transform.rotation);
        blt.GetComponent<Rigidbody2D>().AddForce(transform.up * 50f,ForceMode2D.Force);
    }
    
    private void ShootFromSpotThree()
    {
        GameObject blt = Instantiate(bullet, bulletSpotThree.transform.position, transform.rotation);
        blt.GetComponent<Rigidbody2D>().AddForce(transform.up * 50f,ForceMode2D.Force);
    }
    
    private void FixedUpdate()
    {
        Vector3 cursorPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0f;
        
        pointer.transform.position = cursorPos;
        
        Vector3 dir = cursorPos - transform.position;
        transform.up = dir;
    }

    private IEnumerator Shoot() //Not in use anymore
    {
        while (true)
        {
            GameObject blt = Instantiate(bullet, transform.position, transform.rotation);
            blt.GetComponent<Rigidbody2D>().AddForce(transform.up * 50f,ForceMode2D.Force);

            yield return new WaitForSeconds(1f);
        }
    }

    public void GetOneDmg()
    {
        hp--;

        if (RenifersSpots[0].transform.childCount > 0)
        {
            Destroy(RenifersSpots[0].transform.GetChild(0).gameObject);
        }else if (RenifersSpots[1].transform.childCount > 0)
        {
            Destroy(RenifersSpots[1].transform.GetChild(0).gameObject);
        }else if (RenifersSpots[2].transform.childCount > 0)
        {
            Destroy(RenifersSpots[2].transform.GetChild(0).gameObject);
        }

        if (hp <= 0)
        {
            Die();
        }
    }

    public void HealPlayer()
    {
        if (hp < 3)
        {
            hp++;
            
            if (RenifersSpots[2].transform.childCount == 0)
            {
                Instantiate(reindeer, RenifersSpots[2].transform);
            }else if (RenifersSpots[1].transform.childCount == 0)
            {
                Instantiate(reindeer, RenifersSpots[1].transform);
            }else if (RenifersSpots[0].transform.childCount == 0)
            {
                Instantiate(reindeer, RenifersSpots[0].transform);
                Debug.Log("Player is Dead and this shouldn't even appear");
            }
            
        }
    }
    
    public void Die()
    {
        GameManager.Instance.ChangeStateTo(GameState.EndGameLose);
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            GetOneDmg();
        }
        else if (other.gameObject.CompareTag("Candy"))
        {
            GetOneDmg();
            GetOneDmg();
        }
    }
}
