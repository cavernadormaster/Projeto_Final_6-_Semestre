using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinLobbyAgain : MonoBehaviour
{
    public static bool joinLobbyAgain;
    public GameObject[] Panels;
    void Start()
    {
        if(joinLobbyAgain)
        {
            Panels[0].SetActive(false);
            Panels[1].SetActive(true);
        }

    }

   
}
