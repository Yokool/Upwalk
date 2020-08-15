using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(Moveable))]
[RequireComponent(typeof(OnNextTurnCallback_Base))] /* <-- CallBackBase servers as a marker component for the turn type */
[DisallowMultipleComponent]
public class GridObjectTurnMovement : MonoBehaviour
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

        TileMoverTurnSystem.INSTANCE.RegisterObject(gridObject);

    }

    private void OnDisable()
    {
        TileMoverTurnSystem.INSTANCE.RemoveObject(gridObject);
    }

    public Vector2Int GetNextTile()
    {
        bool checkSuccess = false;

        foreach(ITurnMovementCheck intelligenceCheck in intelligenceChecks)
        {
            bool cache = intelligenceCheck.PerformCheck(); /* I'm aware you don't have to cache, this is more verbose that PerformCheck WILL be called */
            // Go through ALL the checks, even if we fail on the first one
            // we have to check every single one
            checkSuccess = checkSuccess || cache;
        }

        if (!checkSuccess)
        {
            return new Vector2Int(gridObject.X, gridObject.Y);
        }

        return mover.GetTileToMoveTo();

    }
}
