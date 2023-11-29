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
    public float jumpSet;
    public float rotationSet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cientista") || other.CompareTag("Zumbi"))
        {
            ip = other.gameObject.name;
            GameObject g = GameObject.Find(ip);
            NetworkCharacterControllerPrototypeCustom cs = g.GetComponent<NetworkCharacterControllerPrototypeCustom>();
            cs.maxSpeed = speedSet;
            cs.jumpImpulse = jumpSet;
            cs.rotationSpeed = rotationSet;
            playerinRange.Add(g);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Cientista") || other.CompareTag("Zumbi"))
        {
            Debug.Log("Player saiu");
            ip = other.gameObject.name;
            GameObject g = GameObject.Find(ip);
            NetworkCharacterControllerPrototypeCustom cs = g.GetComponent<NetworkCharacterControllerPrototypeCustom>();
            cs.maxSpeed = 2f;
            cs.jumpImpulse = 8.0f;
            cs.rotationSpeed = 5.0f;
            playerinRange.Remove(g);
        }
    }
}
