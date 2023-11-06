using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class PlayerInterations : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnInteractChanged))]
    public bool isTakeInputPressed {get; set;}
    public bool ISFireInputPressed { get; set;}
    public static bool isInItemRange;
    
    public GameObject[] ItensTOSpawn;
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
            if (netWorkInputData.isFireButtonPressed)
            {
                if (!Despertador.FiredRelogio)
                {
                    FireInteract();
                }
            }
        }

    }

    void Interact()
    {
        StartCoroutine(TakeCO());
    }

    void FireInteract()
    {
        Debug.Log("Despertador");
        GameObject temp = GameObject.Find("Despertador(Item Desativado)");
        Destroy(temp);
        GameObject originalGameObject = GameObject.Find(ItensScript.ip);
        GameObject child = originalGameObject.transform.GetChild(0).gameObject;
        Instantiate(ItensTOSpawn[0], child.transform.parent);
        Despertador.FiredRelogio = true;
        StartCoroutine(FireCO());
    }

    IEnumerator FireCO()
    {
        ISFireInputPressed = true;
        Despertador.ThrowObject();
        yield return new WaitForSeconds(0.09f);
        ISFireInputPressed = false;

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
        {
            if (!ItensScript.isItemInHands)
            {
                Debug.Log("DespertadorNO");
                ItensScript.TakeItem();
            }
            else if(ItensScript.isItemInHands && Despertador.FiredRelogio)
            {
                Debug.Log("DespertadorYES");
                Despertador.ThrowObject();
                GameObject temp = GameObject.Find("Despertador(Item Desativado)");
                Destroy(temp);
            }
           
        }
    }
}
