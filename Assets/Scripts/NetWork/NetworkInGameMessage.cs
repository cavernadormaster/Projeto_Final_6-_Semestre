using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkInGameMessage : NetworkBehaviour
{
    public InGameMessageUIHandler InGameMessageUIHandler;

    void Start()
    {
        
    }

    public void SendInGameRPCMessage(string userNickName, string message)
    {
        RPC_InGameMessage($"<b>{userNickName} </b> {message}");
    }

   [Rpc(RpcSources.StateAuthority,RpcTargets.All)]
   void RPC_InGameMessage(string message, RpcInfo info = default)
    {
        Debug.Log($"[RPC] InGameMessage {message}");

        if (InGameMessageUIHandler != null)
            InGameMessageUIHandler.OnGameMessageReceived(message);
    }
}
