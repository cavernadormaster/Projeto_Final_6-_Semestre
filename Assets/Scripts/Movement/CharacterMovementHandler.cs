using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{
    [Header("Animation")]
    public  Animator CharacterAnimation;
    public static Animator PreAnim;
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
            CharacterAnimation = PreAnim;
            anim = false;
        }
    }

    

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetWorkInputData networkInputData) && Runner.IsForward)
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
