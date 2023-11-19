using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Images_appear : MonoBehaviour
{
    public GameObject Imagem_1;
    public GameObject Imagem_2;
    public GameObject Imagem_3;

    void Start()
    {
        StartCoroutine(ShowAndHide(3.0f));
    }
    IEnumerator ShowAndHide(float delay)
    {
        Imagem_1.SetActive(false);
        Imagem_2.SetActive(false);
        Imagem_3.SetActive(false);
        yield return new WaitForSeconds(delay);
        Imagem_1.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        Imagem_2.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        Imagem_3.SetActive(true);
    }
}
