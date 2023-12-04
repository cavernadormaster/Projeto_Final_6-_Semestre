using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despertador : MonoBehaviour
{
    public static Rigidbody m_Rigidbody;
    public static GameObject esteItem;
    public static string ip;

    public AudioSource audioSorce;

    public static string tagNotToAttach;

    [Header("Tempo Para Destruir O Objeto")]
    public float DestroyCooldown;
    void Start()
    {
        audioSorce.Play();
        StartCoroutine(DestroyCoolDown());
    }


    IEnumerator DestroyCoolDown()
    {
        Debug.Log("TIME LEFT TO DESTROY");
        yield return new WaitForSeconds(DestroyCooldown);
    }
    public void ObjectOnHit()
    {
        
        GameObject originalGameObject = GameObject.Find(ip);
        GameObject child = originalGameObject.transform.GetChild(1).gameObject;
        GameObject child2 = child.transform.GetChild(0).gameObject;
        gameObject.transform.position = child2.transform.position;
        gameObject.transform.parent = child2.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cientista Verde") || other.CompareTag("Cientista Vermelho") || other.CompareTag("Cientista Amarelo") || other.CompareTag("Cientista Azul"))
        {
            if (other.name == tagNotToAttach)
                return;

            PlayerInterations.isInItemRange = true;
            ip = other.gameObject.name;
            tagNotToAttach = ip;
            Debug.Log(ip);
            ObjectOnHit();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cientista Verde") || other.CompareTag("Cientista Vermelho") || other.CompareTag("Cientista Amarelo") || other.CompareTag("Cientista Azul"))
        {
           
        }
    }
}
