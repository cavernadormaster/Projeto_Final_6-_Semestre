using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoScrollText : MonoBehaviour
{
    float speed = 45.0f;
    float boundaryTextEnd = 2241.0f;

    RectTransform myCorectTransform;
    [SerializeField]
    TextMeshProUGUI mainText;



    // Start is called before the first frame update
    void Start()
    {
        myCorectTransform = gameObject.GetComponent<RectTransform>();
        StartCoroutine(autoScrollText());
    }


    IEnumerator autoScrollText()
    {
        while (myCorectTransform.localPosition.y < boundaryTextEnd)
        {
            myCorectTransform.Translate(Vector3.up * speed * Time.deltaTime);
            yield return null;
        }
    }
}
