using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    byte[] connectionToken;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (connectionToken == null)
        {
            connectionToken = ConnectionTokensUtil.NewToken();
            Debug.Log($"Player connection token{ConnectionTokensUtil.HashToken(connectionToken)}");
        }
    }

    public void SetConnectionToken(byte[] connectionToken)
    {
        this.connectionToken = connectionToken;
    }

    public byte[] GetConnectionToken()
    {
        return connectionToken;
    }
    
}
