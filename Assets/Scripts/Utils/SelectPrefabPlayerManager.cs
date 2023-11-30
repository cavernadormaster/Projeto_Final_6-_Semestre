using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using Random = UnityEngine.Random;

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
    public bool isCientistBlue { get; set; }

    [Networked(OnChanged = nameof(OnPersonagemChange2))]
    public bool isZumbi { get; set; }

    [Networked(OnChanged = nameof(startedGameChange))]
    public bool  started { get; set; }


    [Header("Animations")] public Animator[] animations;

    [Header("Zumbi Prefab")]public GameObject ZumbiePrefab;
    [Header("Player Prefab")] public GameObject[] Playerprefab;
    [Header("Paredes")] public GameObject parede;

    public static int playersInScene;
   public int playersIn = 0;

    [Header("Lista de Players na plataforma")] public GameObject[] playersNaCena;
    [Header("Spawn Points")] public GameObject[] spawnpoints;

    [Header("Numeros e Falas para começar a partida")] public GameObject[] CountDownToStart;
    [Header("Start Button")] public GameObject startButton;

    public static bool startedGame;

    private void Update()
    {
        


    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetWorkInputData netWorkInputData))
        {
            if (netWorkInputData.isCientistBlue)
            {
                Debug.Log("Cientista");
                if (GameObject.Find("PlayerCientistaAmarelo(clone)") != null || GameObject.Find("PlayerCientistaAzul(clone)") != null || GameObject.Find("PlayerCientistaVerde(clone)") != null || GameObject.Find("PlayerCientistaVermelho(clone)") != null)
                    ChangePersonagem();
            }
            if (netWorkInputData.isZumbi)
            {
                Debug.Log("Zumbie");
                if (GameObject.Find("PlayerZumbi(clone)") != null)
                    ChangePersonagem();
            }

            if(netWorkInputData.started)
            {
                Debug.Log("Started");
                InGameManager.HasStarted = true;
                
            }

           
        }
    }

    void StartedGame()
    {

        if (playersNaCena[0].tag != "Cientista")
        {
            DisableControllers();
            parede.SetActive(false);
            playersNaCena[0].transform.position = spawnpoints[0].transform.position;
            EnableControllers();
        }
        else if (playersNaCena[0] != null && playersNaCena[0].tag == "Cientista")
        {
            DisableControllers();
            parede.SetActive(false);
            playersNaCena[0].transform.position = spawnpoints[UnityEngine.Random.Range(1, 5)].transform.position;
            EnableControllers();
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
                temp.transform.rotation = originalGameObject.transform.rotation;
                temp.transform.SetParent(originalGameObject.transform);
               
            }

            if(corDoCientista == "Amarelo")
            {
                GameObject temp = Instantiate(Playerprefab[1]);
                temp.transform.position = originalGameObject.transform.position;
                temp.transform.rotation = originalGameObject.transform.rotation;
                temp.transform.SetParent(originalGameObject.transform);
                originalGameObject.GetComponent<CharacterMovementHandler>().CharacterAnimation = animations[1];
            }

            if (corDoCientista == "Vermelho")
            {
                GameObject temp = Instantiate(Playerprefab[2]);
                temp.transform.position = originalGameObject.transform.position;
                temp.transform.rotation = originalGameObject.transform.rotation;
                temp.transform.SetParent(originalGameObject.transform);
                originalGameObject.GetComponent<CharacterMovementHandler>().CharacterAnimation = animations[1];
            }

            if (corDoCientista == "Verde")
            {
                GameObject temp = Instantiate(Playerprefab[3]);
                temp.transform.position = originalGameObject.transform.position;
                temp.transform.rotation = originalGameObject.transform.rotation;
                temp.transform.SetParent(originalGameObject.transform);
                originalGameObject.GetComponent<CharacterMovementHandler>().CharacterAnimation = animations[1];
            }

        }
        else if (TipoDePersonagem != "Cientista")
        {
            GameObject originalGameObject = GameObject.Find(ip);
            GameObject child = originalGameObject.transform.GetChild(0).gameObject;
            Destroy(child);
            GameObject temp = Instantiate(ZumbiePrefab);
            temp.transform.position = originalGameObject.transform.position;
            temp.transform.rotation = originalGameObject.transform.rotation;
            temp.transform.SetParent(originalGameObject.transform);
            GameObject child2 = temp.transform.GetChild(0).gameObject;
            originalGameObject.GetComponent<CharacterMovementHandler>().CharacterAnimation = child2.GetComponent<Animator>();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            ip = other.gameObject.name;
            AddToGameObjectsArray(GameObject.Find(ip));
            playersIn++;
            
            parede.SetActive(true);
            TipoDePersonagem = TipoDePlataforma;
           if(TipoDePersonagem == "Cientista")
            {
                InGameManager.CientistInGame++;
                Debug.Log(TipoDePersonagem);
                other.tag = "Cientista";

                isCientistBlue = true;
            }
            if(TipoDePersonagem != "Cientista")
            {
                Debug.Log(TipoDePersonagem);
                other.tag = "Zumbi";
                isZumbi = true;
                
            }
            CheckIfIsServer();
        }
    }

    void AddToGameObjectsArray(GameObject newGameObject)
    {
        if (newGameObject != null)
        {
            // Resize the array to accommodate the new GameObject
            int newSize = playersNaCena.Length + 1;
            GameObject[] newArray = new GameObject[newSize];

            // Copy existing elements to the new array
            for (int i = 0; i < playersNaCena.Length; i++)
            {
                newArray[i] = playersNaCena[i];
            }

            // Add the new GameObject to the end of the array
            newArray[newSize - 1] = newGameObject;

            // Update the reference to the new array
            playersNaCena = newArray;
        }
        else
        {
            Debug.LogError("The provided GameObject is null!");
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
        startButton.SetActive(false);
    }

    public IEnumerator StartCountDown(SessionInfo sessionInfo)
    {
        SessionInfoListUIItem.isOpen = false;
        started = true;
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
        InGameManager.HasStarted = true;
    }

    static void OnPersonagemChange(Changed<SelectPrefabPlayerManager> changed)
    {
       bool isTakeCurrent = changed.Behaviour.isCientistBlue;

        Debug.Log($"{Time.time} OnTakeChanged value Fire {changed.Behaviour.isCientistBlue}");

        changed.LoadOld();

       bool isTakingOld = changed.Behaviour.isCientistBlue;

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
;

        changed.LoadOld();

        bool isTakingOld = changed.Behaviour.started;
;

        if (isTakeCurrent && !isTakingOld)
            changed.Behaviour.StartCountDownUp();
    }

    void StartCountDownUp()
    {
        StartCoroutine(StartCountDown2());
    }

    public IEnumerator StartCountDown2()
    {
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
        InGameManager.HasStarted = true;
        OnStartedGame();
    }

    void OnStartedGame()
    {
        
        if (playersNaCena[0].tag != "Cientista")
        {
            DisableControllers();
            parede.SetActive(false);
            playersNaCena[0].transform.position = spawnpoints[0].transform.position;
            EnableControllers();
        }
        else if (playersNaCena[0] != null && playersNaCena[0].tag == "Cientista")
        {
            DisableControllers();
            parede.SetActive(false);
            playersNaCena[0].transform.position = spawnpoints[UnityEngine.Random.Range(1, 5)].transform.position;
            EnableControllers();
        }
        
    }
    void DisableControllers()
    {
        foreach (GameObject obj in playersNaCena)
        {
            CharacterController controller = obj.GetComponent<CharacterController>();

            if (controller != null)
            {
                controller.enabled = false;
            }
            else
            {
                Debug.LogWarning("CharacterController not found on object: " + obj.name);
            }
        }
    }

    void EnableControllers()
    {
        foreach (GameObject obj in playersNaCena)
        {
            CharacterController controller = obj.GetComponent<CharacterController>();

            if (controller != null)
            {
                controller.enabled = true;
            }
            else
            {
                Debug.LogWarning("CharacterController not found on object: " + obj.name);
            }
        }
    }

    Vector3 RandomPosition()
    {
        float x = Random.Range(-32f, +32f);
        float z = Random.Range(-32f, +32f);
        return new Vector3(x, 32f, z);
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
                temp.transform.rotation = originalGameObject.transform.rotation;
                temp.transform.SetParent(originalGameObject.transform);
                originalGameObject.GetComponent<CharacterMovementHandler>().CharacterAnimation = animations[1];
            }

            if (corDoCientista == "Amarelo")
            {
                GameObject temp = Instantiate(Playerprefab[1]);
                temp.transform.position = originalGameObject.transform.position;
                temp.transform.rotation = originalGameObject.transform.rotation;
                temp.transform.SetParent(originalGameObject.transform);
                originalGameObject.GetComponent<CharacterMovementHandler>().CharacterAnimation = animations[1];
            }

            if (corDoCientista == "Vermelho")
            {
                GameObject temp = Instantiate(Playerprefab[2]);
                temp.transform.position = originalGameObject.transform.position;
                temp.transform.rotation = originalGameObject.transform.rotation;
                temp.transform.SetParent(originalGameObject.transform);
                originalGameObject.GetComponent<CharacterMovementHandler>().CharacterAnimation = animations[1];
            }

            if (corDoCientista == "Verde")
            {
                GameObject temp = Instantiate(Playerprefab[3]);
                temp.transform.position = originalGameObject.transform.position;
                temp.transform.rotation = originalGameObject.transform.rotation;
                temp.transform.SetParent(originalGameObject.transform);
                originalGameObject.GetComponent<CharacterMovementHandler>().CharacterAnimation = animations[1];
            }

        }
        else if (TipoDePersonagem != "Cientista")
        {
            GameObject originalGameObject = GameObject.Find(ip);
            GameObject child = originalGameObject.transform.GetChild(0).gameObject;
            Destroy(child);
            GameObject temp = Instantiate(ZumbiePrefab);
            temp.transform.position = originalGameObject.transform.position;
            temp.transform.rotation = originalGameObject.transform.rotation;
            temp.transform.SetParent(originalGameObject.transform);
            GameObject child2 = temp.transform.GetChild(0).gameObject;
            originalGameObject.GetComponent<CharacterMovementHandler>().CharacterAnimation = child2.GetComponent<Animator>();
        }
    }
}
