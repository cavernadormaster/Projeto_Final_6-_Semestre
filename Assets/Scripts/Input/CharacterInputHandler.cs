using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");
    }

    public NetWorkInputData GetNetWorkInput()
    {
        NetWorkInputData netWorkInputData = new NetWorkInputData();

        netWorkInputData.movementInput = moveInputVector;

        return netWorkInputData;
    }
}
