using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLifetimeManager : MonoBehaviour
{
    private static GameLifetimeManager instance;
    public static GameLifetimeManager INSTANCE => instance;

    private GameSessionData sessionData;
    
    public int GetCoins()
    {
        return sessionData.Coins;
    }

    public void AddCoins(int amount)
    {
        sessionData.Coins += amount;

        CoinCounter.INSTANCE.CoinCounterUpdate();
    }

    public bool GameStarted
    {
        get;
        set;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }


    public void StartGame()
    {
        GridCamera.INSTANCE.StartMovingCamera();

        sessionData = new GameSessionData();

        GameStarted = true;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("DeathScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
