
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoading = 100f;
    [SerializeField]
    private string sceneNameToLoad;

    private float timeElapsed;

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > delayBeforeLoading)
            SceneManager.LoadScene(sceneNameToLoad);
    }
}
