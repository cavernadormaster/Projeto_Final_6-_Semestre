using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class PlayerInterations : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnInteractChanged))]
    public bool isTakeInputPressed {get; set;}
    public static bool isInItemRange;
    void Start()
    {
        
    }

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetWorkInputData netWorkInputData))
        {
            if (isInItemRange)
            {
                Debug.Log("IsInRange");
                if (netWorkInputData.isTakeInputPressed)
                {
                    Interact();
                }
            }
        }

    }

    void Interact()
    {
        StartCoroutine(TakeCO());
    }

    IEnumerator TakeCO()
    {
        isTakeInputPressed = true;
        ItensScript.TakeItem();
        yield return new WaitForSeconds(0.09f);
        isTakeInputPressed = false;
    }

    static void OnInteractChanged(Changed<PlayerInterations> changed)
    {
        Debug.Log($"{Time.time} OnTakeChanged value {changed.Behaviour.isTakeInputPressed}");

        bool isTakeCurrent = changed.Behaviour.isTakeInputPressed;

        changed.LoadOld();

        bool isTakingOld = changed.Behaviour.isTakeInputPressed;

        if (isTakeCurrent && !isTakingOld)
            changed.Behaviour.OnInteractionRemote();
    }

    void OnInteractionRemote()
    {
        if (!Object.HasInputAuthority)
            Debug.Log("Take!");
    }
}
