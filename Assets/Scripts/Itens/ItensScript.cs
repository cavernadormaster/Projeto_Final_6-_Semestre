using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensScript : MonoBehaviour
{
    public static GameObject esteItem;
    public static string ip;
    public static bool isItemInHands;
    void Start()
    {
        esteItem = this.gameObject;
    }

    void Update()
    {
        
    }

    public static void TakeItem()
    {
            GameObject originalGameObject = GameObject.Find(ip);
            GameObject child = originalGameObject.transform.GetChild(0).gameObject;
            esteItem.transform.position = child.transform.position;
            esteItem.transform.parent = child.transform;
            isItemInHands = true;
    }

    public static void DestroyYourSelf()
    {
        Destroy(esteItem);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerInterations.isInItemRange = true;
            ip = other.gameObject.name;
            Debug.Log(ip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInterations.isInItemRange = false;
        }
    }

   
}
