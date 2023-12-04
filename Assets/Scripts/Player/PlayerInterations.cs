using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class PlayerInterations : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnInteractChanged))]
    public bool isTakeInputPressed {get; set;}

    public static bool isInItemRange;

    #region Variaveis de verificação de variaveis da net
    public static bool isTakeCurrent;
    public static bool isTakingOld;
    public static bool hasFirePressed;
    #endregion
    public GameObject[] ItensTOSpawn;

    public static string NomeDoRelogio;
    string NomeDoRelogioArremeçavel;

    bool pegou;
    void Start()
    {
        
    }

    private void Update()
    {
        if(gameObject.tag == "Zumbi")
        {
            gameObject.GetComponent<PlayerInterations>().enabled = false;
        }
    }

  
    

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetWorkInputData netWorkInputData))
        {

            if (isInItemRange)// && !ItensScript.isItemInHands)
            {
                Debug.Log("IsInRange");
                if (netWorkInputData.isTakeInputPressed)
                {
                    Interact();
                }
            }
            
            if (netWorkInputData.segurando)
            {
                Debug.Log("SEGURANDOOOOOOOOO");
            }
        }

    }

    void Interact()
    {
        StartCoroutine(TakeCO());
    }

    IEnumerator TakeCO()
    {
        if (!pegou)
        {
            pegou = true;
            isTakeInputPressed = true;
            isInItemRange = false;
            Debug.Log(NomeDoRelogio);
            GameObject temp = GameObject.Find(NomeDoRelogio);
            Destroy(temp);
            GameObject originalGameObject = GameObject.Find(ItensScript.ip);
            GameObject child = originalGameObject.transform.GetChild(1).gameObject;
            GameObject child2 = child.transform.GetChild(0).gameObject;
            GameObject temp2 = Instantiate(ItensTOSpawn[1], child2.transform.parent);
            temp2.transform.position = child2.transform.position;
            NomeDoRelogioArremeçavel = temp2.name;
        }
        CharacterInputHandler.segurando = true;
        yield return new WaitForSeconds(0.09f);
        pegou = false;
        isTakeInputPressed = false;
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
            Debug.Log(NomeDoRelogio);
            GameObject temp = GameObject.Find(NomeDoRelogio);
            Destroy(temp);
            GameObject originalGameObject = GameObject.Find(ItensScript.ip);
            GameObject child = originalGameObject.transform.GetChild(1).gameObject;
            GameObject child2 = child.transform.GetChild(0).gameObject;
            GameObject temp2 = Instantiate(ItensTOSpawn[1], child2.transform.parent);
            temp2.transform.position = child2.transform.position;
        }
    }
   
}
