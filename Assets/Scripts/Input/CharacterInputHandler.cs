using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;
    Vector2 viewInputVector = Vector2.zero;

   public  CharacterMovementHandler characterMovementHandler;

    bool isJumpButoonPressed = false;
    bool isTakeButtonPressed = false;
    bool isFireButtonPressed = false;
    bool IsWalking = false;
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
        if (!characterMovementHandler.Object.HasInputAuthority)
            return;
        

        Camera.main.transform.position = gameObject.transform.position + new Vector3(0, 5, -5);

        viewInputVector.x = Input.GetAxis("Mouse X");

        characterMovementHandler.SetViewInputVector(viewInputVector);

        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        IsWalking = Input.GetKey(KeyCode.W);

        isJumpButoonPressed = Input.GetKey(KeyCode.Space);
        isTakeButtonPressed = Input.GetKey(KeyCode.Mouse1);
        isFireButtonPressed = Input.GetKey(KeyCode.Mouse0);
    }

    public NetWorkInputData GetNetWorkInput()
    {
        NetWorkInputData netWorkInputData = new NetWorkInputData();

        netWorkInputData.rotationInput = viewInputVector.x;
        netWorkInputData.movementInput = moveInputVector;
        netWorkInputData.isJumpPressed = isJumpButoonPressed;
        netWorkInputData.isTakeInputPressed = isTakeButtonPressed;
        netWorkInputData.isFireButtonPressed = isFireButtonPressed;
        netWorkInputData.Walking = IsWalking;


       // isJumpButoonPressed = false;
        isFireButtonPressed = false;
        isTakeButtonPressed = false;
        
        return netWorkInputData;
    }
}
