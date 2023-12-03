using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;

public class InGameManager : NetworkBehaviour
{
    [Networked(OnChanged = nameof(DoorOnChange))]
    public bool HasAExit { get; set; }

    [Networked(OnChanged = nameof(DoorOnChange2))]
    public bool DOOR1 { get; set; }

    [Networked(OnChanged = nameof(DoorOnChange3))]
    public bool DOOR2 { get; set; }

    [Networked(OnChanged = nameof(DoorOnChange4))]
    public bool DOOR3 { get; set; }

    [Networked(OnChanged = nameof(DoorOnChange5))]
    public bool DOOR4 { get; set; }

    public static int CientistInGame;
    public static bool HasStarted;
    public static bool ZumbiWins;
    public static bool matou = false;
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
    }
    static void DoorOnChange(Changed<InGameManager> changed)
    {
        bool isTakeCurrent = changed.Behaviour.HasAExit;

        changed.LoadOld();

        bool isTakingOld = changed.Behaviour.HasAExit;

        if (isTakeCurrent && !isTakingOld)
            changed.Behaviour.SetDoor();
    }
    static void DoorOnChange2(Changed<InGameManager> changed)
    {
        bool isTakeCurrent = changed.Behaviour.DOOR1;

        Debug.Log($"{Time.time} OnTakeChanged value DOOR1 {changed.Behaviour.DOOR1}");

        changed.LoadOld();

        bool isTakingOld = changed.Behaviour.DOOR1;

        if (isTakeCurrent && !isTakingOld)
            changed.Behaviour.SetDoor1();
    }
    static void DoorOnChange3(Changed<InGameManager> changed)
    {
        bool isTakeCurrent = changed.Behaviour.DOOR2;

        Debug.Log($"{Time.time} OnTakeChanged value DOOR2 {changed.Behaviour.DOOR2}");

        changed.LoadOld();

        bool isTakingOld = changed.Behaviour.DOOR2;

        if (isTakeCurrent && !isTakingOld)
            changed.Behaviour.SetDoor2();
    }
    static void DoorOnChange4(Changed<InGameManager> changed)
    {
        bool isTakeCurrent = changed.Behaviour.DOOR3;

        Debug.Log($"{Time.time} OnTakeChanged value DOOR3 {changed.Behaviour.DOOR3}");

        changed.LoadOld();

        bool isTakingOld = changed.Behaviour.DOOR3;

        if (isTakeCurrent && !isTakingOld)
            changed.Behaviour.SetDoor3();
    }
    static void DoorOnChange5(Changed<InGameManager> changed)
    {
        bool isTakeCurrent = changed.Behaviour.DOOR4;

        Debug.Log($"{Time.time} OnTakeChanged value DOOR4 {changed.Behaviour.DOOR4}");

        changed.LoadOld();

        bool isTakingOld = changed.Behaviour.DOOR4;

        if (isTakeCurrent && !isTakingOld)
            changed.Behaviour.SetDoor4();
    }
    void SetDoor1()
    {
        Debug.Log("Cientista Ganhou Na Saida: " + exits);
        ExitDoors[0].SetActive(true);
    }
    void SetDoor2()
    {
        Debug.Log("Cientista Ganhou Na Saida: " + exits);
        ExitDoors[1].SetActive(true);
    }
    void SetDoor3()
    {
        Debug.Log("Cientista Ganhou Na Saida: " + exits);
        ExitDoors[2].SetActive(true);
    }
    void SetDoor4()
    {
        Debug.Log("Cientista Ganhou Na Saida: " + exits);
        ExitDoors[3].SetActive(true);
    }
    void SetDoor()
      {
        Debug.Log("Cientista Ganhou Na Saida: " + exits);
        ExitDoors[0].SetActive(true);
        // switch(exits)
        // {
        //     case 0:
        //         Debug.Log("Cientista Acionou Na Saida: " + exits);
        //         DOOR1 = true;
        //         HasAExit = false;
        //         break;
        //     case 1:
        //         Debug.Log("Cientista Acionou Na Saida: " + exits);
        //         DOOR2 = true;
        //         HasAExit = false;
        //         break;
        //     case 2:
        //         Debug.Log("Cientista Acionou Na Saida: " + exits);
        //         DOOR3 = true;
        //         HasAExit = false;
        //         break;
        //     case 3:
        //         Debug.Log("Cientista Acionou Na Saida: " + exits);
        //         DOOR4 = true;
        //         HasAExit = false;
        //         break;
        // }
    }
    void startedGameChange()
    {
        if (CientistInGame == 1 && !doorOpened)
        {
           
            if (Spawner.isServer)
            {
                exits = Random.Range(0, 4);
                HasAExit = true;
                doorOpened = true;
            }
            
        }
        if (CientistInGame <= 0 || ZumbiWins)
        {
            
            SceneManager.LoadScene("Vitoria_Zumbi");
        }
    }
}
