using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class PlayerInterations : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnInteractChanged))]
    public bool isTakeInputPressed {get; set;}

    [Networked(OnChanged = nameof(OnFireChanged))]
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
                        GameObject temp = GameObject.Find("Despertador(Item Desativado)");
                        Destroy(temp);
                        GameObject originalGameObject = GameObject.Find(ItensScript.ip);
                        GameObject child = originalGameObject.transform.GetChild(0).gameObject;
                        GameObject temp2 = Instantiate(ItensTOSpawn[0], child.transform.parent);
                        temp2.transform.SetParent(null);
                        Despertador.FiredRelogio = true;
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
        StartCoroutine(FireCO());
    }

    IEnumerator FireCO()
    {
        isFireButtonPressed = true;
        Despertador.ThrowObject();
        yield return new WaitForSeconds(0.09f);
        isFireButtonPressed = true;
    }

    IEnumerator TakeCO()
    {
        isTakeInputPressed = true;
        ItensScript.TakeItem();
        yield return new WaitForSeconds(0.09f);
        isTakeInputPressed = false;
    }

    static void OnFireChanged(Changed<PlayerInterations> changed)
    {
        isTakeCurrent = changed.Behaviour.isFireButtonPressed;

        Debug.Log($"{Time.time} OnTakeChanged value Fire {changed.Behaviour.isFireButtonPressed}");

        changed.LoadOld();

        isTakingOld = changed.Behaviour.isFireButtonPressed;

        if (isTakeCurrent && !isTakingOld)
            changed.Behaviour.OnFireRemote();

    }

    static void OnInteractChanged(Changed<PlayerInterations> changed)
    {
        
        isTakeCurrent = changed.Behaviour.isTakeInputPressed;
        Debug.Log($"{Time.time} OnTakeChanged value {changed.Behaviour.isTakeInputPressed}");
        
        changed.LoadOld();
        
        isTakingOld = changed.Behaviour.isTakeInputPressed;
        
        if (isTakeCurrent && !isTakingOld)
            changed.Behaviour.OnInteractionRemote();
    }

    void OnInteractionRemote()
    {
        if (!Object.HasInputAuthority)
        {
                Debug.Log("DespertadorNO");
                ItensScript.TakeItem();
        }
    }
    void OnFireRemote()
    {
        if (!Object.HasInputAuthority)
        {
            Debug.Log("DespertadorYES");
            GameObject originalGameObject = GameObject.Find(ItensScript.ip);
            GameObject child = originalGameObject.transform.GetChild(0).gameObject;
            GameObject temp2 = Instantiate(ItensTOSpawn[0], child.transform.parent);
            Despertador.ThrowObject();
            GameObject temp = GameObject.Find("Despertador(Item Desativado)");
            Destroy(temp);
        }
    }
}
