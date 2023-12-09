using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowwballScript : MonoBehaviour
{
    private Coroutine dmg;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("zadano dmg ze sniezki");

            if (dmg == null) dmg = StartCoroutine(Dmg(other));
            
        }
    }

    private IEnumerator Dmg(Collider2D other)
    {
        Debug.Log("Corutynka");
        other.gameObject.GetComponent<PlayerScript>().GetOneDmg();
        Destroy(gameObject);
        yield return null;
    }
    
}
