using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{

    private static TurnSystem instance;
    public static TurnSystem INSTANCE => instance;

    private Turn currentTurn = Turn.FULL;
    public Turn CurrentTurn
    {
        get
        {
            return currentTurn;
        }

        private set
        {
            currentTurn = value;
        }
    }

    private GameLifetimeManager gameLifetimeManager;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        gameLifetimeManager = GameLifetimeManager.INSTANCE;
    }

    public void NextTurn()
    {
        if (!gameLifetimeManager.GameStarted)
        {
            return;
        }

        int currentTurn_int = (int)CurrentTurn;
        currentTurn_int++;

        if(currentTurn_int > 3)
        {
            currentTurn_int = 0;
        }

        CurrentTurn = (Turn)currentTurn_int;
        OnNextTurn();
    }

    private void OnNextTurn()
    {
        TurnImageManager.INSTANCE.UpdateTurnSprite();
    }

}
