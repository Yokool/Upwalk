﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLifetimeManager : MonoBehaviour
{
    private static GameLifetimeManager instance;
    public static GameLifetimeManager INSTANCE => instance;

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
        GameStarted = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}