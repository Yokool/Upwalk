using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLifetimeManager : MonoBehaviour
{
    private static GameLifetimeManager instance;
    public static GameLifetimeManager INSTANCE => instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
