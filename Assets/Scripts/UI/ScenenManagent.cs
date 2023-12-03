using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenenManagent : MonoBehaviour
{

    
    public float delayBeforeLoading = 100f;
    [SerializeField]
    private string sceneNameToLoad;

    private float timeElapsed;

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > delayBeforeLoading)
            SceneManager.LoadScene(sceneNameToLoad);
    }

    //public static bool JoinAgain;
    //public void SendToMenu()
    //{
    //    JoinAgain = true;
    //    SceneManager.LoadScene("Menu");
    //}
}
