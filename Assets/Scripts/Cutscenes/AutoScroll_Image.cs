using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll_Image : MonoBehaviour
{
    float speed = 45.0f;

    float image1PosBegin = -914.0f;
    float image2PosBegin = -3006.0f;
    float image3PosBegin = -5056.0f;
    float boundaryImage1End = 954.0f;

    RectTransform myCTransform;

    [SerializeField] Image image1;
    [SerializeField] Image image2;
    [SerializeField] Image image3;



    // Start is called before the first frame update
    void Start()
    {
        myCTransform = gameObject.GetComponent<RectTransform>();
        StartCoroutine(AutoScrollImage1());
    }

    IEnumerator AutoScrollImage1()
    {
        while (myCTransform.localPosition.y < boundaryImage1End)
        {
            myCTransform.Translate(Vector3.up * speed * Time.deltaTime);
            yield return null;
        }

    }
}
