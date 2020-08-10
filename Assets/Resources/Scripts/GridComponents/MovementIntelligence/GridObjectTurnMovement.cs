using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Moveable))]
[RequireComponent(typeof(OnNextTurnCallback_Base))]
[DisallowMultipleComponent]
public class GridObjectTurnMovement : MonoBehaviour, IOnNextTurn_Callback
{

    private ITurnMover[] intelligences;
    private ITurnMovementCheck[] intelligenceChecks;

    private void OnEnable()
    {
        intelligences = GetComponents<ITurnMover>();
        intelligenceChecks = GetComponents<ITurnMovementCheck>();
    }

    public void OnNextTurn()
    {

        bool checkSuccess = false;

        foreach(ITurnMovementCheck intelligenceCheck in intelligenceChecks)
        {
            bool cache = intelligenceCheck.PerformCheck();
            // Go through ALL the checks, even if we fail on the first one
            // we have to check every single one
            checkSuccess = checkSuccess || cache;
        }

        if (!checkSuccess)
        {
            return;
        }

        foreach(ITurnMover intelligence in intelligences)
        {
            intelligence.MoveTile();
        }
        
    }
}
