using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenenManagent : MonoBehaviour
{
    public static bool JoinAgain;
    public void SendToMenu()
    {
        JoinAgain = true;
        SceneManager.LoadScene("Menu");
    }
}
