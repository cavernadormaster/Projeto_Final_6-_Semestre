using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class SelectPrefabPlayerManager : NetworkBehaviour
{

    SessionInfo sessionInfo1;
    public static bool PersonagemSelecionado;
    public string TipoDePersonagem;
    public string corDoCientista;
    private Material myMaterial;
    public string TipoDePlataforma;
    string ip;
    [Networked(OnChanged = nameof(OnPersonagemChange))]
    public bool isCientist { get; set; }

    [Networked(OnChanged = nameof(OnPersonagemChange2))]
    public bool isZumbi { get; set; }

    [Networked(OnChanged = nameof(startedGameChange))]
    public bool started { get; set; }

    [Header("Zumbi Prefab")]public GameObject ZumbiePrefab;
    [Header("Player Prefab")] public GameObject[] Playerprefab;
    [Header("Paredes")] public GameObject parede;

    public static int playersInScene;
    int playersIn = 0;

    [Header("Lista de Players na plataforma")] public GameObject[] playersNaCena;
    [Header("Spawn Points")] public GameObject[] spawnpoints;

    [Header("Numeros e Falas para começar a partida")] public GameObject[] CountDownToStart;
    [Header("Start Button")] public GameObject startButton;



    private void Update()
    {
       
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetWorkInputData netWorkInputData))
        {
            if (netWorkInputData.isCientist)
            {
                Debug.Log("Cientista");
                ChangePersonagem();
            }
            if (netWorkInputData.isZumbi)
            {
                Debug.Log("Zumbie");
                ChangePersonagem();
            }

            if(netWorkInputData.started)
            {
                Debug.Log("Started");
                StartedGame();
            }
        }
    }

    void StartedGame()
    {
        if (playersNaCena[0].tag != "Cientista")
        {
            parede.SetActive(false);
            playersNaCena[0].transform.position = spawnpoints[0].transform.position;
        }
        else if (playersNaCena[0] != null)
        {
            parede.SetActive(false);
            playersNaCena[0].transform.position = spawnpoints[UnityEngine.Random.Range(1, 5)].transform.position;
        }
        if (playersNaCena[1].tag != "Cientista" && playersNaCena[1] != null)
        {
            parede.SetActive(false);
            playersNaCena[1].transform.position = spawnpoints[0].transform.position;
        }
        else if (playersNaCena[1] != null)
        {
            parede.SetActive(false);
            playersNaCena[1].transform.position = spawnpoints[UnityEngine.Random.Range(1, 5)].transform.position;
        }
        if (playersNaCena[2].tag != "Cientista" && playersNaCena[2] != null)
        {
            parede.SetActive(false);
            playersNaCena[2].transform.position = spawnpoints[0].transform.position;
        }
        else if (playersNaCena[2] != null)
        {
            parede.SetActive(false);
            playersNaCena[2].transform.position = spawnpoints[UnityEngine.Random.Range(1, 5)].transform.position;
        }
        if (playersNaCena[3].tag != "Cientista" && playersNaCena[3] != null)
        {
            parede.SetActive(false);
            playersNaCena[3].transform.position = spawnpoints[0].transform.position;
        }
        else if (playersNaCena[3] != null)
        {
            parede.SetActive(false);
            playersNaCena[3].transform.position = spawnpoints[UnityEngine.Random.Range(1, 5)].transform.position;
        }
        if (playersNaCena[4].tag != "Cientista" && playersNaCena[4] != null)
        {
            parede.SetActive(false);
            playersNaCena[4].transform.position = spawnpoints[0].transform.position;
        }
        else if (playersNaCena[4] != null)
        {
            parede.SetActive(false);
            playersNaCena[4].transform.position = spawnpoints[UnityEngine.Random.Range(1, 5)].transform.position;
        }
    }

    void ChangePersonagem()
    {
        if (TipoDePersonagem == "Cientista")
        {
            GameObject originalGameObject = GameObject.Find(ip);
            GameObject child = originalGameObject.transform.GetChild(0).gameObject;
            Destroy(child);
            

            if (corDoCientista == "Azul")
            {
                GameObject temp = Instantiate(Playerprefab[0]);
                temp.transform.position = originalGameObject.transform.position;
                temp.transform.SetParent(originalGameObject.transform);
            }

            if(corDoCientista == "Amarelo")
            {
                GameObject temp = Instantiate(Playerprefab[1]);
                temp.transform.position = originalGameObject.transform.position;
                temp.transform.SetParent(originalGameObject.transform);
            }

            if (corDoCientista == "Vermelho")
            {
                GameObject temp = Instantiate(Playerprefab[2]);
                temp.transform.position = originalGameObject.transform.position;
                temp.transform.SetParent(originalGameObject.transform);
            }

            if (corDoCientista == "Verde")
            {
                GameObject temp = Instantiate(Playerprefab[3]);
                temp.transform.position = originalGameObject.transform.position;
                temp.transform.SetParent(originalGameObject.transform);
            }

        }
        else if (TipoDePersonagem != "Cientista")
        {
            GameObject originalGameObject = GameObject.Find(ip);
            GameObject child = originalGameObject.transform.GetChild(0).gameObject;
            Destroy(child);
            GameObject temp = Instantiate(ZumbiePrefab);
            temp.transform.position = originalGameObject.transform.position;
            temp.transform.SetParent(originalGameObject.transform);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            ip = other.gameObject.name;
            playersNaCena[playersIn] = (GameObject.Find(ip));
            playersIn++;
            parede.SetActive(true);
            TipoDePersonagem = TipoDePlataforma;
           if(TipoDePersonagem == "Cientista")
            {
                Debug.Log(TipoDePersonagem);
                other.tag = "Cientista";
                isCientist = true;
            }
            if(TipoDePersonagem != "Cientista")
            {
                Debug.Log(TipoDePersonagem);
                other.tag = "Zumbi";
                isZumbi = true;
               
            }

            if(playersIn >=1)
            {
                CheckIfIsServer();
            }
        }
    }
    void CheckIfIsServer()
    {
        if (Spawner.isServer)
            startButton.SetActive(true);
    }

    public void StartCountDownEnumerator()
    {
        StartCoroutine(StartCountDown(sessionInfo1));
    }

    public IEnumerator StartCountDown(SessionInfo sessionInfo)
    {
        SessionInfoListUIItem.isOpen = false;
        CountDownToStart[0].SetActive(true);
        yield return new WaitForSeconds(3f);
        CountDownToStart[0].SetActive(false);
        CountDownToStart[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        CountDownToStart[1].SetActive(false);
        CountDownToStart[2].SetActive(true);
        yield return new WaitForSeconds(1f);
        CountDownToStart[2].SetActive(false);
        CountDownToStart[3].SetActive(true);
        yield return new WaitForSeconds(1f);
        CountDownToStart[3].SetActive(false);
        CountDownToStart[4].SetActive(true);
        yield return new WaitForSeconds(1f);
        CountDownToStart[4].SetActive(false);
        CountDownToStart[5].SetActive(true);
        yield return new WaitForSeconds(1f);
        CountDownToStart[5].SetActive(false);
        CountDownToStart[6].SetActive(true);
        yield return new WaitForSeconds(1f);
        CountDownToStart[6].SetActive(false);
        started = true;
       
    }

    static void OnPersonagemChange(Changed<SelectPrefabPlayerManager> changed)
    {
       bool isTakeCurrent = changed.Behaviour.isCientist;

        Debug.Log($"{Time.time} OnTakeChanged value Fire {changed.Behaviour.isCientist}");

        changed.LoadOld();

       bool isTakingOld = changed.Behaviour.isCientist;

        if (isTakeCurrent && !isTakingOld)
            changed.Behaviour.OnChangeRemote();

        
    }

    static void OnPersonagemChange2(Changed<SelectPrefabPlayerManager> changed)
    {
        bool isTakeCurrent = changed.Behaviour.isZumbi;

        Debug.Log($"{Time.time} OnTakeChanged value Fire {changed.Behaviour.isZumbi}");

        changed.LoadOld();

        bool isTakingOld = changed.Behaviour.isZumbi;

        if (isTakeCurrent && !isTakingOld)
            changed.Behaviour.OnChangeRemote();

        
    }

    static void startedGameChange(Changed<SelectPrefabPlayerManager> changed)
    {
        bool isTakeCurrent = changed.Behaviour.started;

        changed.LoadOld();

        bool isTakingOld = changed.Behaviour.started;

        if (isTakeCurrent && !isTakingOld)
            changed.Behaviour.OnStartedGame();
    }

    void OnStartedGame()
    {
        if (playersNaCena[0].tag != "Cientista")
        {
            parede.SetActive(false);
            playersNaCena[0].transform.position = spawnpoints[0].transform.position;
        }
        else if (playersNaCena[0] != null)
        {
            parede.SetActive(false);
            playersNaCena[0].transform.position = spawnpoints[UnityEngine.Random.Range(1, 5)].transform.position;
        }
        if (playersNaCena[1].tag != "Cientista" && playersNaCena[1] != null)
        {
            parede.SetActive(false);
            playersNaCena[1].transform.position = spawnpoints[0].transform.position;
        }
        else if (playersNaCena[1] != null)
        {
            parede.SetActive(false);
            playersNaCena[1].transform.position = spawnpoints[UnityEngine.Random.Range(1, 5)].transform.position;
        }
        if (playersNaCena[2].tag != "Cientista" && playersNaCena[2] != null)
        {
            parede.SetActive(false);
            playersNaCena[2].transform.position = spawnpoints[0].transform.position;
        }
        else if (playersNaCena[2] != null)
        {
            parede.SetActive(false);
            playersNaCena[2].transform.position = spawnpoints[UnityEngine.Random.Range(1, 5)].transform.position;
        }
        if (playersNaCena[3].tag != "Cientista" && playersNaCena[3] != null)
        {
            parede.SetActive(false);
            playersNaCena[3].transform.position = spawnpoints[0].transform.position;
        }
        else if (playersNaCena[3] != null)
        {
            parede.SetActive(false);
            playersNaCena[3].transform.position = spawnpoints[UnityEngine.Random.Range(1, 5)].transform.position;
        }
        if (playersNaCena[4].tag != "Cientista" && playersNaCena[4] != null)
        {
            parede.SetActive(false);
            playersNaCena[4].transform.position = spawnpoints[0].transform.position;
        }
        else if (playersNaCena[4] != null)
        {
            parede.SetActive(false);
            playersNaCena[4].transform.position = spawnpoints[UnityEngine.Random.Range(1, 5)].transform.position;
        }
    }

    void OnChangeRemote()
    {
        if (TipoDePersonagem == "Cientista")
        {
            GameObject originalGameObject = GameObject.Find(ip);
            GameObject child = originalGameObject.transform.GetChild(0).gameObject;
            Destroy(child);


            if (corDoCientista == "Azul")
            {
                GameObject temp = Instantiate(Playerprefab[0]);
                temp.transform.position = originalGameObject.transform.position;
                temp.transform.SetParent(originalGameObject.transform);
            }

            if (corDoCientista == "Amarelo")
            {
                GameObject temp = Instantiate(Playerprefab[1]);
                temp.transform.position = originalGameObject.transform.position;
                temp.transform.SetParent(originalGameObject.transform);
            }

            if (corDoCientista == "Vermelho")
            {
                GameObject temp = Instantiate(Playerprefab[2]);
                temp.transform.position = originalGameObject.transform.position;
                temp.transform.SetParent(originalGameObject.transform);
            }

            if (corDoCientista == "Verde")
            {
                GameObject temp = Instantiate(Playerprefab[3]);
                temp.transform.position = originalGameObject.transform.position;
                temp.transform.SetParent(originalGameObject.transform);
            }

        }
        else if (TipoDePersonagem != "Cientista")
        {
            GameObject originalGameObject = GameObject.Find(ip);
            GameObject child = originalGameObject.transform.GetChild(0).gameObject;
            Destroy(child);
            GameObject temp = Instantiate(ZumbiePrefab);
            temp.transform.position = originalGameObject.transform.position;
            temp.transform.SetParent(originalGameObject.transform);
        }
    }
}
