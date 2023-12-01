using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{
    [Header("Animation")]
    public  Animator CharacterAnimation;
    public static bool anim;
    Vector2 viewInput;
    float walkSpeed = 0;

    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;

    private void Awake()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
       
    }

    void Start()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
    }

    void Update()
    {
        if(anim)
        {
            Debug.Log("PEGOU");
            anim = false;
        }
    }

    

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetWorkInputData networkInputData))
        {
            Vector3 moveDirection = transform.forward * networkInputData.movementInput.y + transform.right * networkInputData.movementInput.x;
            moveDirection.Normalize();

            networkCharacterControllerPrototypeCustom.Move(moveDirection);
            
            CharacterAnimation.SetBool("IsWalking", networkInputData.isJumpPressed);         

            if (networkInputData.Walking)
                networkCharacterControllerPrototypeCustom.Jump();
        }
    }



    public void SetViewInputVector(Vector2 viewInput)
    {
        this.viewInput = viewInput;
    }
}
