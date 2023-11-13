using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerInternInteractions : MonoBehaviour
{
    public static string ip;
    public List<GameObject> playerinRange = new List<GameObject>();
    bool isinRange;
    int i = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isinRange)
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ip = other.gameObject.name;
            GameObject g = GameObject.Find(ip);
            playerinRange.Add(g);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player saiu");
            ip = other.gameObject.name;
            GameObject g = GameObject.Find(ip);
            playerinRange.Remove(g);
        }
    }
    
       
}
