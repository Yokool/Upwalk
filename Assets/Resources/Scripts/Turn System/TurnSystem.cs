using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private Dictionary<Turn, List<IOnNextTurn_Callback>> turnCallbacks;

    private void Awake()
    {
        instance = this;

        turnCallbacks = new Dictionary<Turn, List<IOnNextTurn_Callback>>();

        // Initiate all turn values
        foreach (Turn value in System.Enum.GetValues(typeof(Turn)))
        {
            turnCallbacks[value] = new List<IOnNextTurn_Callback>();
        }

    }

    public void RegisterNextTurnCallback(Turn turn, IOnNextTurn_Callback onNextTurnCallback)
    {
        turnCallbacks[turn].Add(onNextTurnCallback);
    }

    public void UnregisterNextTurnCallback(Turn turn, IOnNextTurn_Callback onNextTurnCallback)
    {
        turnCallbacks[turn].Remove(onNextTurnCallback);
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

        List<IOnNextTurn_Callback> callbacks = turnCallbacks[CurrentTurn];
        foreach(IOnNextTurn_Callback callback in callbacks)
        {
            callback.OnNextTurn();
        }

    }

}
