using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPassScenes : MonoBehaviour
{
    public GameObject[] Mortes;
    void Start()
    {
        StartCoroutine(PassScenes());
    }

   IEnumerator PassScenes()
    {
        Mortes[0].SetActive(true);
        yield return new WaitForSeconds(3f);
        Mortes[0].SetActive(false);
        Mortes[1].SetActive(true);
        yield return new WaitForSeconds(3f);
        Mortes[1].SetActive(false);
        Mortes[2].SetActive(true);
        yield return new WaitForSeconds(3f);
        Mortes[2].SetActive(false);
    }
}
