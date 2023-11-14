using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class Spawner : MonoBehaviour, INetworkRunnerCallbacks
{
    
    public NetWorkPlayer[] playerPrefab;

    Dictionary<int, NetWorkPlayer> mapTokenIDWithNetworkPlayer;

    CharacterInputHandler characterInputHandler;

    void Start()
    {
        
    }

    private void Awake()
    {
        mapTokenIDWithNetworkPlayer = new Dictionary<int, NetWorkPlayer>();
    }

    int GetPlayerToken(NetworkRunner runner, PlayerRef player)
    {
        if(runner.LocalPlayer == player)
        {
            return ConnectionTokensUtil.HashToken(GameManager.instance.GetConnectionToken());
        }
        else
        {
            var token = runner.GetPlayerConnectionToken(player);

            if (token != null)
                return ConnectionTokensUtil.HashToken(token);

            Debug.LogError($"GetPlayerToken return invalid token");

            return 0; // invalid
        }
    }

    public void SetConnectionTokenMapping(int token, NetWorkPlayer netWorkPlayer)
    {
        mapTokenIDWithNetworkPlayer.Add(token, netWorkPlayer);
    }

   public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) 
   {
       if (runner.IsServer)
       {
            int playerToken = GetPlayerToken(runner, player);

           Debug.Log($"OnplayerJoined we are server. Connection token {playerToken}");

            if(mapTokenIDWithNetworkPlayer.TryGetValue(playerToken, out NetWorkPlayer netWorkPlayer))
            {
                Debug.Log($"Found old connection token for token {playerToken}. Assigning controlls to that player");

                netWorkPlayer.GetComponent<NetworkObject>().AssignInputAuthority(player);
            }
            else
            {
                Debug.Log($"Spawning new player for connection token {playerToken}");

                NetWorkPlayer spawnedNetworkPlayer = runner.Spawn(playerPrefab[0], Utils.GetRandomSpawnPoint(), Quaternion.identity, player);

                spawnedNetworkPlayer.token = playerToken;

                mapTokenIDWithNetworkPlayer[playerToken] = spawnedNetworkPlayer;
            }

           //runner.Spawn(playerPrefab[0], Utils.GetRandomSpawnPoint(), Quaternion.identity, player);
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

        FindObjectOfType<NetWorkRuunigHandler>().StartHostMigration(hostmigrationtoken);
    }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }

   public void OnHostMigrationCleanUp()
    {
        Debug.Log("Spawner OnHostMigrationCleanUp started");

        foreach(KeyValuePair<int, NetWorkPlayer> entry in mapTokenIDWithNetworkPlayer)
        {
            NetworkObject netWorkPlayerDictionary = entry.Value.GetComponent<NetworkObject>();

            if(netWorkPlayerDictionary.InputAuthority.IsNone)
            {
                Debug.Log($"{Time.time} Found player thet has not reconnected. Despawning {entry.Value.nickName}");

                netWorkPlayerDictionary.Runner.Despawn(netWorkPlayerDictionary);
            }
        }

        Debug.Log("Spawner OnHostMigrationCleanUp Completed");
    }

    

    
}
