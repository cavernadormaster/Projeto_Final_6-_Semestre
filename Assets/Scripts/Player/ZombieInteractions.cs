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

            if (!characterMovementHandler.Object.HasInputAuthority)
                GameObject.Find("Morte").SetActive(true);

            ip = other.gameObject.name;

            Destroy(GameObject.Find(ip));
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
