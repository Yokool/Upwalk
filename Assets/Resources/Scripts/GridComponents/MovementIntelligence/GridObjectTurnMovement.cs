﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(Moveable))]
[RequireComponent(typeof(OnNextTurnCallback_Base))] /* <-- CallBackBase servers as a marker component for the turn type */
[DisallowMultipleComponent]
public class GridObjectTurnMovement : MonoBehaviour, IOnNextTurn_Callback
{

    private ITurnMover mover;
    private ITurnMovementCheck[] intelligenceChecks;
    private OnNextTurnCallback_Base onNextTurnBase;
    private GridObject gridObject;

    private void OnEnable()
    {
        mover = GetComponent<ITurnMover>();
        intelligenceChecks = GetComponents<ITurnMovementCheck>();
        onNextTurnBase = GetComponent<OnNextTurnCallback_Base>();
        gridObject = GetComponent<GridObject>();

    }

    public void MoveToNextTile()
    {
        // Auto success when there are no checks
        bool checkSuccess = (intelligenceChecks.Length == 0);

        foreach(ITurnMovementCheck intelligenceCheck in intelligenceChecks)
        {
            bool cache = intelligenceCheck.PerformCheck(); /* I'm aware you don't have to cache, this is more verbose that PerformCheck WILL be called */
            // Go through ALL the checks, even if we fail on the first one
            // we have to check every single one
            checkSuccess = checkSuccess || cache;
        }

        if (!checkSuccess)
        {
            return;
        }

        mover.GetTileToMoveTo();

    }

    public void OnNextTurn()
    {
        MoveToNextTile();
    }
}
