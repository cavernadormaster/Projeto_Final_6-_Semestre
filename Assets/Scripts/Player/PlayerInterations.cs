using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class PlayerInterations : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnInteractChanged))]
    public bool isTakeInputPressed {get; set;}
    public bool isFireButtonPressed { get; set;}
    public static bool isInItemRange;

    #region Variaveis de verificação de variaveis da net
    public static bool isTakeCurrent;
    public static bool isTakingOld;
    public static bool hasFirePressed;
    #endregion
    public GameObject[] ItensTOSpawn;
    void Start()
    {
        
    }

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetWorkInputData netWorkInputData))
        {
            if (isInItemRange && !ItensScript.isItemInHands)
            {
                Debug.Log("IsInRange");
                if (netWorkInputData.isTakeInputPressed)
                {
                    Interact();
                }
            }
            if (!Despertador.FiredRelogio)
            {
                if (netWorkInputData.isFireButtonPressed)
                {
                    FireInteract();
                    hasFirePressed = true;
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
        GameObject temp = GameObject.Find("Despertador(Item Desativado)");
        Destroy(temp);
        GameObject originalGameObject = GameObject.Find(ItensScript.ip);
        GameObject child = originalGameObject.transform.GetChild(0).gameObject;
        GameObject temp2 = Instantiate(ItensTOSpawn[0], child.transform.parent);
        temp2.transform.SetParent(null);
        Despertador.FiredRelogio = true;
        StartCoroutine(FireCO());
    }

    IEnumerator FireCO()
    {
        isFireButtonPressed = true;
        Despertador.ThrowObject();
        yield return new WaitForSeconds(0.09f);
        isFireButtonPressed = false;

    }

    IEnumerator TakeCO()
    {
        Debug.Log("TAKE CONTROL");
        isTakeInputPressed = true;
        ItensScript.TakeItem();
        yield return new WaitForSeconds(0.09f);
        isTakeInputPressed = false;
    }


    static void OnInteractChanged(Changed<PlayerInterations> changed)
    {

        if (!ItensScript.isItemInHands)
        {
            isTakeCurrent = changed.Behaviour.isTakeInputPressed;
            Debug.Log($"{Time.time} OnTakeChanged value {changed.Behaviour.isTakeInputPressed}");
        }else if(hasFirePressed)
        {
            isTakeCurrent = changed.Behaviour.isFireButtonPressed;
            Debug.Log($"{Time.time} OnTakeChanged value Fire {changed.Behaviour.isFireButtonPressed}"); 
        }

        changed.LoadOld();

        if (!ItensScript.isItemInHands)
        {
            Debug.Log($"{Time.time} OnTakeChanged value {changed.Behaviour.isTakeInputPressed}");
            isTakingOld = changed.Behaviour.isTakeInputPressed;
        }
        else if (hasFirePressed)
        {
            Debug.Log($"{Time.time} OnTakeChanged value Fire {changed.Behaviour.isFireButtonPressed}");
            isTakingOld = changed.Behaviour.isFireButtonPressed;
        }


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
            else if(ItensScript.isItemInHands && hasFirePressed)
            {
                Debug.Log("DespertadorYES");
                GameObject originalGameObject = GameObject.Find(ItensScript.ip);
                GameObject child = originalGameObject.transform.GetChild(0).gameObject;
                GameObject temp2 = Instantiate(ItensTOSpawn[0], child.transform.parent);
                temp2.transform.SetParent(null);
                Despertador.ThrowObject();
                Despertador.FiredRelogio = true;
                GameObject temp = GameObject.Find("Despertador(Item Desativado)");
                Destroy(temp);
            }
           
        }
    }
}
