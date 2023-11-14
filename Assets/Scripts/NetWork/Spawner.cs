using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class Spawner : MonoBehaviour, INetworkRunnerCallbacks
{
    
    public NetWorkPlayer[] playerPrefab;

    CharacterInputHandler characterInputHandler;

    void Start()
    {
        
    }

   public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) 
   {
       if (runner.IsServer)
       {
           Debug.Log("OnplayerJoined we are server. Spawning player");
           runner.Spawn(playerPrefab[0], Utils.GetRandomSpawnPoint(), Quaternion.identity, player);
       }
       else Debug.Log("OnPlayerJoined");
   }
   public void OnInput(NetworkRunner runner, NetworkInput input)
   {
        if (characterInputHandler == null && NetWorkPlayer.Local != null)
            characterInputHandler = NetWorkPlayer.Local.GetComponent<CharacterInputHandler>();

        if(characterInputHandler != null)
           input.Set(characterInputHandler.GetNetWorkInput());

   }

    
    public void OnConnectedToServer(NetworkRunner runner){Debug.Log("OnConnectedToServer");}
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdown) { Debug.Log("OnShutdown"); }
    public void OnDisconnectedFromServer(NetworkRunner runner) { Debug.Log("OnDisconnectedFromServer"); }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { Debug.Log("OnconnectedRequest"); }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { Debug.Log("OnConnectedFailed");  }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public async void OnHostMigration(NetworkRunner runner, HostMigrationToken hostmigrationtoken) 
    {
        Debug.Log("OnHostMigration");

        await runner.Shutdown(shutdownReason: ShutdownReason.HostMigration);

        FindAnyObjectByType<NetWorkRuunigHandler>().StartHostMigration(hostmigrationtoken);
    }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }

   

    

    
}
