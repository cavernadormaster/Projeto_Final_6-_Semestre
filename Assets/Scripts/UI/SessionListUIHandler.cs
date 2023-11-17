using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using TMPro;

public class SessionListUIHandler : MonoBehaviour
{
    public TextMeshProUGUI statusText;
    public GameObject sessionInfoListPrefab;
    public VerticalLayoutGroup verticalLayoutGroup;

    private void Awake()
    {
        ClearList();
    }

    public void ClearList()
    {
        foreach(Transform child in verticalLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }

        statusText.gameObject.SetActive(false);
    }

    public void AddList(SessionInfo sessionInfo)
    {
        SessionInfoListUIItem addedSessionInfoUIItem = 
            Instantiate(sessionInfoListPrefab, verticalLayoutGroup.transform).GetComponent<SessionInfoListUIItem>();

        addedSessionInfoUIItem.SetInformation(sessionInfo);

        addedSessionInfoUIItem.OnjoinSession += AddedSessionInfoUIItem_OnJoinSession;
    }

    void AddedSessionInfoUIItem_OnJoinSession(SessionInfo obj)
    {

    }

    public void InNoSessionFound()
    {
        statusText.text = "No game session found";
        statusText.gameObject.SetActive(true);
    }

    public void OnLookingForGameSession()
    {
        statusText.text = "Looking for game sessions";
        statusText.gameObject.SetActive(true);
    }
}
