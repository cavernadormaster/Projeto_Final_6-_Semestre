using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despertador : MonoBehaviour
{
    public static Rigidbody m_Rigidbody;
    public static GameObject esteItem;
    public float m_Thrust = 20f;
    public static bool isThrowing;
    public static string ip;
    void Start()
    {
        esteItem = this.gameObject;
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(isThrowing)
        {
            transform.SetParent(null);
            m_Rigidbody.AddForce((transform.forward * m_Thrust) + (transform.up * m_Thrust));
            isThrowing = false;
        }
    }

    public static void ThrowObject()
    {
        isThrowing = true;
    }

    public void ObjectOnHit()
    {
        GameObject originalGameObject = GameObject.Find(ip);
        esteItem.transform.parent = originalGameObject.transform;
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
