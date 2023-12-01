using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class CharacterMovementHandler : NetworkBehaviour
{
    [Header("Animation")]
    public  Animator CharacterAnimation;
    Vector2 viewInput;
    float walkSpeed = 0;

    

    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;

    private void Awake()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
       
    }

    private void Start()
    {
        NetworkObject thisObject = GetComponent<NetworkObject>();

        if(thisObject.HasStateAuthority)
        {
            GetComponent<CharacterInputHandler>().enabled = true;
        }
    }

    void Update()
    {
       
    }

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetWorkInputData networkInputData))
        {
            Vector3 moveDirection = transform.forward * networkInputData.movementInput.y + transform.right * networkInputData.movementInput.x;
            moveDirection.Normalize();

            networkCharacterControllerPrototypeCustom.Move(moveDirection);

            if (networkInputData.isJumpPressed)
                networkCharacterControllerPrototypeCustom.Jump();

            
            CharacterAnimation.SetBool("IsWalking", networkInputData.Walking);
        }
    }

   

    
    public void SetViewInputVector(Vector2 viewInput)
    {
        this.viewInput = viewInput;
    }
}
