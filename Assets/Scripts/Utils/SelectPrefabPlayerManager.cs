using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPrefabPlayerManager : MonoBehaviour
{
    public GameObject Paredes;
    public GameObject BotãoAcionado; 
    bool isExitTrue;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
                Debug.Log("Entrou");
                Paredes.SetActive(true);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Saiu");
            Paredes.SetActive(false);
        }
    }
}
