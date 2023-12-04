using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class ItensScript : MonoBehaviour
{
    public static GameObject esteItem;
    public static string ip;
    public static bool isItemInHands;
    public string ObjectName;

    public GameObject[] ItensTOSpawn;

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

    public void TakeItem()
    {
        Debug.Log(ObjectName);
        GameObject originalGameObject = GameObject.Find(ip);
        GameObject child = originalGameObject.transform.GetChild(1).gameObject;
        GameObject child2 = child.transform.GetChild(0).gameObject;
        GameObject temp2 = Instantiate(ItensTOSpawn[0], child2.transform.parent);
        temp2.transform.position = child2.transform.position;
        Despertador.tagNotToAttach = ip;
        GameObject temp = GameObject.Find(ObjectName);
        Destroy(temp);

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
            ip = other.gameObject.name;
            TakeItem();
            Debug.Log(ip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cientista Verde") || other.CompareTag("Cientista Vermelho") || other.CompareTag("Cientista Amarelo") || other.CompareTag("Cientista Azul"))
        {

        }
    }

   
}
