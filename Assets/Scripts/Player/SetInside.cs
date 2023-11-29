using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInside : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        if (!CharacterInputHandler.characterMovementHandler.Object.HasInputAuthority)
            return;
        CharacterMovementHandler.CharacterAnimation = animator;
    }

    
}
