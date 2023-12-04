using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class ItensScript : NetworkBehaviour
{
    public static GameObject esteItem;
    public static string ip;
    public static bool isItemInHands;
    public string ObjectName;

    

    void Start()
    {
        esteItem = this.gameObject;
    }

    private void Awake()
    {
        esteItem = this.gameObject;
    }

    void Update()
    {
        
    }

    public static void TakeItem()
    {
        GameObject originalGameObject = GameObject.Find(ip);
        GameObject child = originalGameObject.transform.GetChild(1).gameObject;
        GameObject child2 = child.transform.GetChild(0).gameObject;
        esteItem.transform.position = child2.transform.position;
        esteItem.transform.parent = child2.transform;
        isItemInHands = true;
        Despertador.tagNotToAttach = ip;

    }

    public static void DestroyYourSelf()
    {
        Destroy(esteItem);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cientista Verde") || other.CompareTag("Cientista Vermelho") 
            || other.CompareTag("Cientista Amarelo") || other.CompareTag("Cientista Azul"))
        {
            PlayerInterations.isInItemRange = true;
            ip = other.gameObject.name;
            PlayerInterations.NomeDoRelogio = ObjectName;
            Debug.Log(ip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cientista Verde") || other.CompareTag("Cientista Vermelho") || other.CompareTag("Cientista Amarelo") || other.CompareTag("Cientista Azul"))
        {
            PlayerInterations.isInItemRange = false;
        }
    }

   
}
