using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;
    Vector2 viewInputVector = Vector2.zero;

    CharacterMovementHandler characterMovementHandler;

    bool isJumpButoonPressed = false;

    private void Awake()
    {
        characterMovementHandler = GetComponent<CharacterMovementHandler>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        viewInputVector.x = Input.GetAxis("Mouse X");

        characterMovementHandler.SetViewInputVector(viewInputVector);

        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        isJumpButoonPressed = Input.GetKey(KeyCode.Space);

    }

    public NetWorkInputData GetNetWorkInput()
    {
        NetWorkInputData netWorkInputData = new NetWorkInputData();

        netWorkInputData.rotationInput = viewInputVector.x;
        netWorkInputData.movementInput = moveInputVector;
        netWorkInputData.isJumpPressed = isJumpButoonPressed;
        return netWorkInputData;
    }
}
