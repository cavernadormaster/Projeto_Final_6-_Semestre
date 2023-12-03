using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esc : MonoBehaviour
{
    public GameObject Options;
    bool escOn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !escOn)
        {
            Options.SetActive(true);
            escOn = true;
            Debug.Log("1");
        }
        //if (Input.GetKeyDown(KeyCode.Escape) && escOn)
        //{
        //    Options.SetActive(false);
        //    escOn = false;
        //    Debug.Log("2");
        //}
    }
}
