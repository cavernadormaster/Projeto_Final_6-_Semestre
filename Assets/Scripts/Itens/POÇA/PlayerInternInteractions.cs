using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerInternInteractions : MonoBehaviour
{
    public static string ip;
    public GameObject[] playerinRange;
    bool isinRange;
    int i = 0;
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
            playerinRange[i] = GameObject.Find(ip);
            i++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ip = other.gameObject.name;
            GameObject foundObject = FindObjectByName(ip);
            for (int i = 0; i < playerinRange.Length; i++)
            {
                if (playerinRange[i].name == ip)
                {
                    Destroy(playerinRange[i]); // Destroy the object
                                              // Shift the remaining elements in the array to fill the gap
                    for (int j = i; j < playerinRange.Length - 1; j++)
                    {
                        playerinRange[j] = playerinRange[j + 1];
                    }
                    Array.Resize(ref playerinRange, playerinRange.Length - 1);
                    break; // Exit the loop once the object is found and deleted
                }
            }
        }
    }
    
        GameObject FindObjectByName(string nameToFind)
    {
        return playerinRange.FirstOrDefault(obj => obj.name == nameToFind);
    }
}
