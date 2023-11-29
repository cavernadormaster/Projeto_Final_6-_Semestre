using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieInteractions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cientista"))
        {
            InGameManager.CientistInGame--;
            string ip = other.gameObject.name;
            Destroy(GameObject.Find(ip));
        }
    }
}
