using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInside : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        CharacterMovementHandler.CharacterAnimation = animator;
    }

    
}
