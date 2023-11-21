using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fusion;
using System;

public class SessionInfoListUIItem : MonoBehaviour
{
    public TextMeshProUGUI sessionNameText;
    public TextMeshProUGUI playerCountText;
    public Button joinButton;
    const int PLAYERMAX = 5;


    SessionInfo sessioninfo;
    


    public event Action<SessionInfo> OnjoinSession;

    public void SetInformation(SessionInfo sessionInfo)
    {
        
        this.sessioninfo = sessionInfo;

        sessionNameText.text = sessionInfo.Name;
        playerCountText.text = $"{sessionInfo.PlayerCount.ToString()}/{PLAYERMAX.ToString()}";

        bool joinButtonActive = true;

        if (sessionInfo.PlayerCount >= PLAYERMAX || sessionInfo.IsOpen)
            joinButtonActive = false;

        joinButton.gameObject.SetActive(joinButtonActive);
    }

    public void OnClick()
    {
        OnjoinSession?.Invoke(sessioninfo);
    }
}
