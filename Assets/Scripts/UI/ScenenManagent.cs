using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenenManagent : MonoBehaviour
{
    public void SendToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
