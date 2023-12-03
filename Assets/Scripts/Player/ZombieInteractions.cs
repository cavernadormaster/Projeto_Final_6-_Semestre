using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieInteractions : MonoBehaviour
{
    string ip;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cientista Verde") || other.CompareTag("Cientista Vermelho") || other.CompareTag("Cientista Amarelo") || other.CompareTag("Cientista Azul"))
        {
            
            ip = other.gameObject.name;
            Destroy(GameObject.Find(ip));

         // if(other.GetComponent<CharacterMovementHandler>().Object.HasInputAuthority)
         //     GameObject.Find("Morte").SetActive(true);
           //
           // if(!InGameManager.matou)
           // StartCoroutine(MorteCountDown());

        }
    }

    IEnumerator MorteCountDown()
    {
        InGameManager.matou = true;
        InGameManager.CientistInGame--;
        
        yield return new WaitForSeconds(2f);
        InGameManager.matou = false;
    }
}
