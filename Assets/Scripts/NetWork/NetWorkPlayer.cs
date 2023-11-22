using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class NetWorkPlayer : NetworkBehaviour, IPlayerLeft
{
    public TextMeshProUGUI playerNickNameTM;
    public static NetWorkPlayer Local { get; set; }
    [Networked(OnChanged = nameof(OnNickNameChanged))]
    public NetworkString<_16> nickName { get; set; }

    [Networked] public int token { get; set; }


    bool isPublicJoinMessageSent = false;

    public GameObject localUI;

    NetworkInGameMessage networkInGameMessage;

    void Awake()
    {
        networkInGameMessage = GetComponent<NetworkInGameMessage>();    
    }

    void Start()
    {

    }

    public override void Spawned()
    {
        SelectPrefabPlayerManager.playersInScene++;
        if (Object.HasInputAuthority)
        {
            Local = this;

            RPC_SetNickName(PlayerPrefs.GetString("PlayerNickname"));
        }
        else
        {
            localUI.SetActive(false);
        }

        Runner.SetPlayerObject(Object.InputAuthority, Object);

        transform.name = $"P_{Object.Id}";
    }
    public void PlayerLeft(PlayerRef player)
    {
        SelectPrefabPlayerManager.playersInScene--;
        if (Object.HasStateAuthority)
        {
            if (Runner.TryGetPlayerObject(player, out NetworkObject playerLeftNetworkObject))
            {
                if (playerLeftNetworkObject == Object)
                    Local.GetComponent<NetworkInGameMessage>().SendInGameRPCMessage(playerLeftNetworkObject.GetComponent<NetWorkPlayer>().nickName.ToString(), "left");
            }
        }


        if (player == Object.InputAuthority)
            Runner.Despawn(Object);
    }

    static void OnNickNameChanged(Changed<NetWorkPlayer> changed)
    {
        Debug.Log($"{Time.time} OnHPChanged value {changed.Behaviour.nickName}");

        changed.Behaviour.OnNickNameChanged();
    }

    private void OnNickNameChanged()
    {
        Debug.Log($"Nick Name changed for player to {nickName} for player {gameObject.name}");

        playerNickNameTM.text = nickName.ToString();
    }


    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetNickName(string nickName, RpcInfo info = default)
    {
        Debug.Log($"[RPC] SetNickName {nickName}");
        this.nickName = nickName;

        if(!isPublicJoinMessageSent)
        {
            networkInGameMessage.SendInGameRPCMessage(nickName, "joined");

            isPublicJoinMessageSent = true;
        }
    }

}
