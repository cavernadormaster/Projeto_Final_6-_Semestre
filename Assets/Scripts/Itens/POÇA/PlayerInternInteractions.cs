using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerInternInteractions : MonoBehaviour
{
    public static string ip;
    public List<GameObject> playerinRange = new List<GameObject>();
    public float speedSet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ip = other.gameObject.name;
            GameObject g = GameObject.Find(ip);
            NetworkCharacterControllerPrototypeCustom cs = g.GetComponent<NetworkCharacterControllerPrototypeCustom>();
            cs.maxSpeed = speedSet;
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
            NetworkCharacterControllerPrototypeCustom cs = g.GetComponent<NetworkCharacterControllerPrototypeCustom>();
            cs.maxSpeed = 2f;
            playerinRange.Remove(g);
        }
    }
    
       
}
