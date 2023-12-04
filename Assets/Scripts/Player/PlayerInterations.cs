using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class PlayerInterations : NetworkBehaviour
{
   

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

  
    

    
   

   
   
}
