using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensScript : MonoBehaviour
{
    public static GameObject esteItem;
    void Start()
    {
        esteItem = this.gameObject;
    }

    void Update()
    {
        
    }

    public static void TakeItem()
    {
        Destroy(esteItem);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerInterations.isInItemRange = true;
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
