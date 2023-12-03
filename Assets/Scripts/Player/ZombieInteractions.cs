using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieInteractions : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cientista Verde") || other.CompareTag("Cientista Vermelho") || other.CompareTag("Cientista Amarelo") || other.CompareTag("Cientista Azul"))
        {
            InGameManager.matou = true;
            string ip = other.gameObject.name;
            if (other.GetComponent<CharacterMovementHandler>().HasInputAuthority)
                GameObject.Find("Morte").SetActive(true);

            Debug.Log("Destroy" + ip);
            Destroy(GameObject.Find(ip));
        }
    }
}
