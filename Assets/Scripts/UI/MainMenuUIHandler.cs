using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUIHandler : MonoBehaviour
{
    [Header("Panels")]
    public GameObject playerDetailsPanel;
    public GameObject sessionBrowserPanel;
    public GameObject createSessionPanel;
    public GameObject statusPanel;
    public GameObject menuPanel;
    public GameObject creditosPanel;
    public GameObject optionPanel;
    public GameObject botoesPanel;

    [Header("Player settings")]
    public TMP_InputField playerNameInputField;

    [Header("New game session")]
    public TMP_InputField sessionNameInputField;

    void Start()
    {
        if(PlayerPrefs.HasKey("PlayerNickname"))
            playerNameInputField.text = PlayerPrefs.GetString("PlayerNickname");

        //if (ScenenManagent.JoinAgain)
         //   OnFindGameClicked();
    }

    void HideAllPanels()
    {
        playerDetailsPanel.SetActive(false);
        sessionBrowserPanel.SetActive(false);
        statusPanel.SetActive(false);
        createSessionPanel.SetActive(false);
        creditosPanel.SetActive(false);
        optionPanel.SetActive(false);

    }

    

    public  void OnFindGameClicked()
    {
        PlayerPrefs.SetString("PlayerNickname", playerNameInputField.text);
        PlayerPrefs.Save();
        NetWorkRuunigHandler netWorkRuunigHandler = FindObjectOfType<NetWorkRuunigHandler>();

        netWorkRuunigHandler.OnJoinLobby();

        HideAllPanels();
        botoesPanel.SetActive(false);
        sessionBrowserPanel.SetActive(true);

        FindObjectOfType<SessionListUIHandler>(true).OnLookingForGameSession();
        //ScenenManagent.JoinAgain = false;
    }

    public void OnCreateNewGameClicked()
    {
        HideAllPanels();
        createSessionPanel.SetActive(true);
    }

    public void OnCreditsClicked()
    {
        HideAllPanels();
        creditosPanel.SetActive(true);
        //ScenenManagent.JoinAgain = true;
    }

    public void OnOptionsClicked()
    {
        HideAllPanels();
        optionPanel.SetActive(true);
        //ScenenManagent.JoinAgain = true;
    }

    public void OnBackClicked()
    {
        HideAllPanels();
        menuPanel.SetActive(true);
        //ScenenManagent.JoinAgain = true;
    }

    public void OnStartNewSessionClicked()
    {
        NetWorkRuunigHandler netWorkRuunigHandler = FindObjectOfType<NetWorkRuunigHandler>();

        netWorkRuunigHandler.CreateGAme(sessionNameInputField.text, "Jogo");

        HideAllPanels();

        statusPanel.gameObject.SetActive(true);
    }
    
    public void OnJoiningServer()
    {
        HideAllPanels();

        statusPanel.gameObject.SetActive(true);
    }

    public void OnExitClicked()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}
