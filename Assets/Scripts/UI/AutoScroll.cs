using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoScroll : MonoBehaviour
{

    float speed = 100.0f;
    float textPosBegin = -1017.408f;
    float boundaryTextEnd = -40.0f;

    RectTransform myCorectTransform;
    [SerializeField]
    TextMeshProUGUI mainText;
    



    // Start is called before the first frame update
    void Start()
    {
        myCorectTransform = gameObject.GetComponent<RectTransform>();
        StartCoroutine(AutoScrollText());
    }

    IEnumerator AutoScrollText()
    {
        while(myCorectTransform.localPosition.y < boundaryTextEnd)
        {
            myCorectTransform.Translate(Vector3.up * speed * Time.deltaTime);
            yield return (null);
        } 
    }

}
