using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

public class InGameManager : NetworkBehaviour
{
    

    public static int CientistInGame;
    public static bool HasStarted;
    public static bool ZumbiWins;
    bool doorOpened;
    public GameObject[] ExitDoors;

    private void Update()
    {
        Debug.Log("Cientista In Game: " + CientistInGame);
            if (HasStarted)
            {
                startedGameChange();
            }
    }

    void startedGameChange()
    {
        if (CientistInGame == 1 && !doorOpened)
        {
            Debug.Log("Cientista Ganhou");
            ExitDoors[Random.Range(0, 4)].SetActive(true);
            doorOpened = true;
        }

        if (CientistInGame <= 0 || ZumbiWins)
        {
            SceneManager.LoadScene("Vitoria_Zumbi");
        }
    }
}
