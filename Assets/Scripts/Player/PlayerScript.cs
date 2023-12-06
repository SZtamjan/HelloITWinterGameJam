using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int hp = 3;
    [SerializeField] private List<GameObject> Renifers;

    public void GetOneDmg()
    {
        hp--;
        Destroy(Renifers[0]);
        Renifers.Remove(Renifers[0]);
    }
    
    
    
    public void GetDmg(int value)
    {
        
    }
}
