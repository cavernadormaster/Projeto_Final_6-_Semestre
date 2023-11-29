using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInside : MonoBehaviour
{
    
    public Animator animator;

    private void Awake()
    {
        
        CharacterMovementHandler.PreAnim = animator;
        CharacterMovementHandler.anim = true;
    }



}
