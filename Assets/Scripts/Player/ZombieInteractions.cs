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

            if (other.GetComponent<CharacterMovementHandler>().HasInputAuthority)
                GameObject.Find("Morte").SetActive(true);

            if(!InGameManager.matou)
            StartCoroutine(MorteCountDown());
            
        }
    }

    IEnumerator MorteCountDown()
    {
        InGameManager.matou = true;
        InGameManager.CientistInGame--;
        GameObject.Find(ip).SetActive(false);
        yield return new WaitForSeconds(2f);
        InGameManager.matou = false;
    }
}
