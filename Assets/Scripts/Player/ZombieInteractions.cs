using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieInteractions : MonoBehaviour
{
    string ip;
    CharacterMovementHandler characterMovementHandler;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cientista Verde") || other.CompareTag("Cientista Vermelho") || other.CompareTag("Cientista Amarelo") || other.CompareTag("Cientista Azul"))
        {

            characterMovementHandler = other.GetComponent<CharacterMovementHandler>();
            ip = other.gameObject.name;
            StartCoroutine(MorteCountDown());
        }
    }

    IEnumerator MorteCountDown()
    {
       
        InGameManager.matou = true;
        InGameManager.CientistInGame--;
        Destroy(GameObject.Find(ip));
        if (characterMovementHandler.Object.HasInputAuthority)
            GameObject.Find("Morte").SetActive(true);
        yield return new WaitForSeconds(2f);
        InGameManager.matou = false;
    }
}
