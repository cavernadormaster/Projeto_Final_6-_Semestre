using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;
    Vector2 viewInputVector = Vector2.zero;

   public  CharacterMovementHandler characterMovementHandler;

    bool isJumpButoonPressed = false;
    bool IsWalking = false;

    public static bool segurando;
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
        Camera.main.transform.position = gameObject.transform.position + new Vector3(0, 7, -7);
        viewInputVector.x = Input.GetAxis("Mouse X");
        characterMovementHandler.SetViewInputVector(viewInputVector);
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");
        IsWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || 
                    Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow);
        isJumpButoonPressed = Input.GetKey(KeyCode.Space);
    }

    public NetWorkInputData GetNetWorkInput()
    {
        NetWorkInputData netWorkInputData = new NetWorkInputData();

        netWorkInputData.rotationInput = viewInputVector.x;
        netWorkInputData.movementInput = moveInputVector;
        netWorkInputData.isJumpPressed = isJumpButoonPressed;
        netWorkInputData.Walking = IsWalking;
        netWorkInputData.segurando = segurando;

        isJumpButoonPressed = false;
        
        return netWorkInputData;
    }
}
