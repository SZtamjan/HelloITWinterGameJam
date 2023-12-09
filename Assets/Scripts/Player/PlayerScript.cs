using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject pointer;

    [SerializeField] private int hp = 3;
    [SerializeField] private List<GameObject> Renifers;

    //Shooting
    private Coroutine shootCor;
    private float czas = 0f;
    [SerializeField] private float playerGunCoolDown = 1f;

    private void Start()
    {
        //StartCoroutine(Shoot());
    }

    private void Update()
    {
        czas += Time.deltaTime;
        if (Input.GetMouseButton(0) && czas > playerGunCoolDown)
        {
            GameObject blt = Instantiate(bullet, transform.position, transform.rotation);
            blt.GetComponent<Rigidbody2D>().AddForce(transform.up * 50f,ForceMode2D.Force);
            czas = 0f;
        }
    }
    
    private void FixedUpdate()
    {
        Vector3 cursorPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0f;
        
        pointer.transform.position = cursorPos;
        
        Vector3 dir = cursorPos - transform.position;
        transform.up = dir;
    }

    private IEnumerator Shoot()
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
        Destroy(Renifers[0]);
        Renifers.Remove(Renifers[0]);
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
        }
    }
    
    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            Die();
        }
        else if (other.gameObject.CompareTag("Candy"))
        {
            Die();
        }
    }
}
