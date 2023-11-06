using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despertador : MonoBehaviour
{
    public static Rigidbody m_Rigidbody;
    public static GameObject esteItem;
    public static float m_Thrust = 20f;
    public static bool isThrowing;
    public static string ip;
    void Start()
    {
        esteItem = this.gameObject;
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    public static void ThrowObject()
    {
        isThrowing = true;

        if (isThrowing)
        {
            esteItem.transform.SetParent(null);
            m_Rigidbody.AddForce((esteItem.transform.forward * m_Thrust) + (esteItem.transform.up * m_Thrust));
            isThrowing = false;
            PlayerInterations.FiredRelogio = true;
        }
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
        if (other.CompareTag("Player"))
        {
            PlayerInterations.isInItemRange = true;
            ip = other.gameObject.name;
            Debug.Log(ip);
            ObjectOnHit();
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
