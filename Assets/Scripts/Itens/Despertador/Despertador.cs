using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despertador : MonoBehaviour
{
    public static Rigidbody m_Rigidbody;
    public static GameObject esteItem;
    public static float m_Thrust = 10f ;
    public static float m_Thrust_Up = 5f;
    public static bool isThrowing;
    public static bool FiredRelogio;
    public static string ip;

    public AudioSource audioSorce;

    [Header("Tempo Para Destruir O Objeto")]
    public float DestroyCooldown;
    void Start()
    {
        audioSorce.Play();
        StartCoroutine(DestroyCoolDown());
    }

    private void Update()
    {
        
    }

    public static void ThrowObject()
    {
       Destroy(GameObject.Find("Despertador(Item Desativado) (1)"));
       esteItem = GameObject.Find("Despertador(Item Ativado)(Clone)");
       m_Rigidbody = esteItem.GetComponent<Rigidbody>();

        Vector3 forceAdd = esteItem.transform.forward * m_Thrust + esteItem.transform.up * m_Thrust_Up;  
       m_Rigidbody.AddForce(forceAdd, ForceMode.Impulse);
       esteItem.transform.SetParent(null);
       FiredRelogio = true;
        

    }
    IEnumerator DestroyCoolDown()
    {
        Debug.Log("TIME LEFT TO DESTROY");
        yield return new WaitForSeconds(DestroyCooldown);
        Destroy(this.gameObject);
    }
    public void ObjectOnHit()
    {
        GameObject originalGameObject = GameObject.Find(ip);
        GameObject child = originalGameObject.transform.GetChild(1).gameObject;
        Destroy(m_Rigidbody);       
        esteItem.transform.position = child.transform.position;
        esteItem.transform.parent = child.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cientista Verde") || other.CompareTag("Cientista Vermelho") || other.CompareTag("Cientista Amarelo") || other.CompareTag("Cientista Azul"))
        {
            PlayerInterations.isInItemRange = true;
            ip = other.gameObject.name;
            Debug.Log(ip);
            ObjectOnHit();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cientista Verde") || other.CompareTag("Cientista Vermelho") || other.CompareTag("Cientista Amarelo") || other.CompareTag("Cientista Azul"))
        {
            PlayerInterations.isInItemRange = false;
        }
    }
}
