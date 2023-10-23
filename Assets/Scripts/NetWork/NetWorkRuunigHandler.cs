using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;
using Fusion.Sockets;
using System.Threading.Tasks;
using System;
using System.Linq;

public class NetWorkRuunigHandler : MonoBehaviour
{
    public NetworkRunner networkRunnerPrefab;
    NetworkRunner networkRunner;

    void Start()
    {
        networkRunner = Instantiate(networkRunnerPrefab);
        networkRunner.name = "Net Test";

        var clientTask = IniTializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient, 
                                                 NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);

        Debug.Log("Server NetWork Started");
    }

   protected virtual Task IniTializeNetworkRunner(NetworkRunner runner, GameMode gameMode, NetAddress address, SceneRef scene, Action<NetworkRunner> initialized)
    {
        var scenemanager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();

        if(scenemanager == null)
        {
            scenemanager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>(); 
        }

        runner.ProvideInput = true;

        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Address = address,
            Scene = scene,
            SessionName = "SampleScene",
            Initialized = initialized,
            SceneManager = scenemanager
        });
    }

}
