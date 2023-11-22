using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class SelectPrefabPlayerManager : NetworkBehaviour
{
    int playersIn;
    int playersInGame;
    public static bool PersonagemSelecionado;
    public string TipoDePersonagem;
    public string corDoCientista;
    private Material myMaterial;
    public string TipoDePlataforma;
    string ip;
    [Networked(OnChanged = nameof(OnPersonagemChange))]
    public bool isCientist { get; set; }

    [Networked(OnChanged = nameof(OnPersonagemChange))]
    public bool isZumbi { get; set; }

    [Header("Zumbi Prefab")]public GameObject ZumbiePrefab;

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
            GameObject child = originalGameObject.transform.GetChild(1).gameObject;
            Material childmaterial = child.GetComponent<Material>();

            if(corDoCientista == "Azul")
            {
                childmaterial.color = Color.blue;
            }

            if(corDoCientista == "Amarelo")
            {
                childmaterial.color = Color.yellow;
            }

            if (corDoCientista == "Vermelho")
            {
                childmaterial.color = Color.red;
            }

            if (corDoCientista == "Verde")
            {
                childmaterial.color = Color.green;
            }

        }
        else if (TipoDePersonagem == "Zumbi")
        {
            GameObject originalGameObject = GameObject.Find(ip);
            GameObject child = originalGameObject.transform.GetChild(1).gameObject;
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
            TipoDePersonagem = TipoDePlataforma;
            ip = other.gameObject.name;
           if(TipoDePersonagem == "Cientista")
            {
                Debug.Log(TipoDePersonagem);
                NetWorkInputData netWorkInputData = new NetWorkInputData();
                netWorkInputData.isCientist = true;
            }
            if(TipoDePersonagem != "Cientista")
            {
                Debug.Log(TipoDePersonagem);
                NetWorkInputData netWorkInputData = new NetWorkInputData();
                netWorkInputData.isZumbi = true;
               
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TipoDePersonagem = "Cientista";
        if (other.CompareTag("Player"))
        {
            Debug.Log("Saiu");
            ip = other.gameObject.name;
            if (TipoDePersonagem == "Cientista")
            {
                NetWorkInputData netWorkInputData = new NetWorkInputData();
                netWorkInputData.isCientist = true;
            }
            else if (TipoDePersonagem == "Zumbi")
            {
                NetWorkInputData netWorkInputData = new NetWorkInputData();
                netWorkInputData.isZumbi = true;
            }
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

        if(isTakeCurrent && isTakingOld)
            changed.Behaviour.OnChangeRemote();
    }

    void OnChangeRemote()
    {
        if (TipoDePersonagem == "Cientista")
        {
            GameObject originalGameObject = GameObject.Find(ip);
            GameObject child = originalGameObject.transform.GetChild(1).gameObject;
            Material childmaterial = child.GetComponent<Material>();

            if (corDoCientista == "Azul")
            {
                childmaterial.color = Color.blue;
            }

            if (corDoCientista == "Amarelo")
            {
                childmaterial.color = Color.yellow;
            }

            if (corDoCientista == "Vermelho")
            {
                childmaterial.color = Color.red;
            }

            if (corDoCientista == "Verde")
            {
                childmaterial.color = Color.green;
            }

        }
        else if (TipoDePersonagem == "Zumbi")
        {
            GameObject originalGameObject = GameObject.Find(ip);
            GameObject child = originalGameObject.transform.GetChild(1).gameObject;
            Destroy(child);
            GameObject temp = Instantiate(ZumbiePrefab);
            temp.transform.position = originalGameObject.transform.position;
            temp.transform.SetParent(originalGameObject.transform);
        }
    }
}
