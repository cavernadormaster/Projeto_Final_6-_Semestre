using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInside : MonoBehaviour
{
    
    public static Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }



}
