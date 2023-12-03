using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieInteractions : MonoBehaviour
{
    string ip;
    CharacterMovementHandler characterMovementHandler;
    public GameObject Mortes;

    private void Awake()
    {
        GameObject ori = GameObject.Find("Main Camera");
        GameObject child = ori.transform.GetChild(0).gameObject;
        GameObject child2 = child.transform.GetChild(9).gameObject;
        Mortes = child2;
    }

    private void Start()
    {
        GameObject ori = GameObject.Find("Main Camera");
        GameObject child = ori.transform.GetChild(0).gameObject;
        GameObject child2 = child.transform.GetChild(9).gameObject;
        Mortes = child2;
    }
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
        if (characterMovementHandler.Object.HasInputAuthority)
        {
            Mortes.SetActive(true);
        }
        Destroy(GameObject.Find(ip));
        yield return new WaitForSeconds(2f);
        InGameManager.matou = false;
    }
}
