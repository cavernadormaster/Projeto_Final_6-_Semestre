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
    public static bool isOpen = true;
    const int PLAYERMAX = 5;

    public static int playercount;
    SessionInfo sessioninfo1;
    
    

    public event Action<SessionInfo> OnjoinSession;

    public void SetIsOpen()
    {
        sessioninfo1.IsOpen = false;
    }

    public void SetInformation(SessionInfo sessionInfo)
    {
        
        this.sessioninfo1 = sessionInfo;

        sessionNameText.text = sessionInfo.Name;
        playerCountText.text = $"{sessionInfo.PlayerCount.ToString()}/{PLAYERMAX.ToString()}";

        bool joinButtonActive = true;
        

        if (sessionInfo.PlayerCount >= PLAYERMAX || !sessioninfo1.IsOpen)
            joinButtonActive = false;

        playercount = sessionInfo.PlayerCount;
        joinButton.gameObject.SetActive(joinButtonActive);
    }

    public void OnClick()
    {
        OnjoinSession?.Invoke(sessioninfo1);
    }
}
