using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfRange : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cientista"))
        {
            other.transform.position = new Vector3(0, 9, 0);
        }
    }
}
