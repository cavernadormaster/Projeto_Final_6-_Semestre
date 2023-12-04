using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float elapsTime;

    // Update is called once per frame
    void Update()
    {
        if (InGameManager.HasStarted)
        {
           
            elapsTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsTime / 60);
            int seconds = Mathf.FloorToInt(elapsTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
