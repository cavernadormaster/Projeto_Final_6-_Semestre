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
    public static bool matou;
    bool doorOpened;
    public GameObject[] ExitDoors;
    int exits;

    private void Update()
    {
        Debug.Log("Cientista In Game: " + CientistInGame);
        if (HasStarted)
        {
            startedGameChange();
        }

        if(matou)
        {
            CientistInGame--;
            matou = false;    
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetWorkInputData networkInputData))
        {
            if(networkInputData.HasAExit)
               ExitDoors[networkInputData.door].SetActive(true);
        }
    }

    void startedGameChange()
    {
        if (CientistInGame == 1 && !doorOpened)
        {
            Debug.Log("Cientista Ganhou");
            if (Spawner.isServer)
            {
                exits = Random.Range(0, 4);
                ExitDoors[exits].SetActive(true);

            }
            doorOpened = true;
        }

        if (CientistInGame <= 0 || ZumbiWins)
        {
            
            SceneManager.LoadScene("Vitoria_Zumbi");
        }
    }

    public NetWorkInputData GetNetWorkInput()
    {
        NetWorkInputData netWorkInputData = new NetWorkInputData();

        netWorkInputData.door = exits;
        netWorkInputData.HasAExit = doorOpened;

        return netWorkInputData;
    }
}
