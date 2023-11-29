using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

public class InGameManager : NetworkBehaviour
{
    public static int CientistInGame;
    public static bool HasStarted;
    public GameObject[] ExitDoors;

    private void Update()
    {
        Debug.Log("Cientista In Game: " + CientistInGame);
        if (GetInput(out NetWorkInputData netWorkInputData))
        {
            if (CientistInGame == 1 && netWorkInputData.started)
            {
                Debug.Log("Cientista Ganhou");
                ExitDoors[Random.Range(0, 4)].SetActive(true);
            }

            if (CientistInGame <= 0 && netWorkInputData.started)
            {
                SceneManager.LoadScene("Vitoria_Zumbi");
            }
        }

    }
}
