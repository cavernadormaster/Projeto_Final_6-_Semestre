using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class SelectPrefabPlayerManager : NetworkBehaviour
{

    SessionInfo sessionInfo1;
    NetworkRunner runner1;
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

    [Header("Zumbi Prefab")]public GameObject ZumbiePrefab;
    [Header("Player Prefab")] public GameObject[] Playerprefab;
    [Header("Paredes")] public GameObject parede;

    public static int playersInScene;
    int playersIn;

    [Header("Lista de Players na plataforma")] public List<GameObject> playersNaCena = new List<GameObject>();

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
            playersIn++;
            parede.SetActive(true);
            TipoDePersonagem = TipoDePlataforma;
            ip = other.gameObject.name;
            playersNaCena.Add(GameObject.Find(ip));
           if(TipoDePersonagem == "Cientista")
            {
                Debug.Log(TipoDePersonagem);
                isCientist = true;
            }
            if(TipoDePersonagem != "Cientista")
            {
                Debug.Log(TipoDePersonagem);
                isZumbi = true;
               
            }

            if(playersIn >=2)
            {

                CheckIfIsServer(runner1);
            }
        }
    }
    void CheckIfIsServer(NetworkRunner runner)
    {
        if (runner.IsServer)
            startButton.SetActive(true);
    }

    public void StartCountDownEnumerator()
    {
        StartCoroutine(StartCountDown(sessionInfo1));
    }

    public IEnumerator StartCountDown(SessionInfo sessionInfo)
    {
        sessionInfo.IsOpen = false;
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
        
        foreach(GameObject obj in playersNaCena)
        {
            CheckTag(obj);
        }

    }

    void CheckTag(GameObject obj)
    {
        if (obj != null)
        {
            string tag = obj.tag;
            if(tag != "Cientista")
            {
                obj.transform.position = new Vector3(-9.48f, 0, 1.63f);
            }
            else
            {
                obj.transform.position = new Vector3(UnityEngine.Random.Range(-45.3f, 41.4f), UnityEngine.Random.Range(0, 1), UnityEngine.Random.Range(-44.8f, 41.5f));
            }
        }
        else
        {
            Debug.LogWarning("GameObject is null!");
        }
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
