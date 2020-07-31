using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(GridObject))]
public class Moveable : MonoBehaviour
{

    [SerializeField]
    private bool shouldLerp = false;
    /// <summary>
    /// Movement blocked determines if all behaviour inside MoveObject should be called.
    /// 
    /// At this point, movement can only be blocked if we're in the middle of a interpolation.
    /// 
    /// </summary>
    private bool movementBlocked = false;

    [SerializeField]
    private bool canGoThroughSolids = false;

    [SerializeField]
    private float lerpIncrementor;

    private GridObject gridObject = null;

    [SerializeField]
    private int step = 0;

    private IObjectArrivalCallback[] objectArrivalCallbacks;
    private IFailedToMoveToCallback failedToMoveToCallback;
    private IObjectPreArrivalCallback[] objectPreArrivalCallbacks;

    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();
        objectArrivalCallbacks = GetComponents<IObjectArrivalCallback>();
        failedToMoveToCallback = GetComponent<IFailedToMoveToCallback>();
        objectPreArrivalCallbacks = GetComponents<IObjectPreArrivalCallback>();
    }

    /*   *******  The 3 next methods are somewhat a copypaste with different implementation  *******  */
    /*   *******  If it will need cleaning in the future use delegates  *******  */
    public void MoveObject_RelativeDirectional(Direction direction)
    {

        if (movementBlocked)
        {
            return;
        }

        if (shouldLerp)
        {
            StartCoroutine(PerformMoveObjectLerp_RelativeDirectional(direction));
            return;
        }
        else
        {
            PerformMoveObject_RelativeDirectional(direction);
        }

    }

    public void MoveObject_Absolute(int X, int Y)
    {
        if (movementBlocked)
        {
            return;
        }

        if (shouldLerp)
        {
            StartCoroutine(PerformMoveObjectLerp_Absolute(X, Y));
            return;
        }
        else
        {
            PerformMoveObject_Absolute(X, Y);
        }
    }

    public void MoveObject_Relative(int X, int Y)
    {
        if (movementBlocked)
        {
            return;
        }

        if (shouldLerp)
        {
            StartCoroutine(PerformMoveObjectLerp_Relative(X, Y));
            return;
        }
        else
        {
            PerformMoveObject_Relative(X, Y);
        }
    }

    public bool CanMoveTo(int X, int Y)
    {
        GridObject[] objectsAt = GameGrid.INSTANCE.ObjectsAt(X, Y);
        bool canMoveTo = canGoThroughSolids || objectsAt.Where((gridObj) => {
            return !TileTypeDataDatabase.TileTypeDatabase[gridObj.m_TileType].walkthrough;
        }).ToArray().Length == 0;

        // Can Go through solids or
        // Filter to all the ones we can't walk through and if there is one say no
        return canMoveTo;
    }

    private void PerformMoveObject_Absolute(int X, int Y)
    {

        int endX = X;
        int endY = Y;

        if (!CanMoveTo(endX, endY))
        {
            failedToMoveToCallback.FailedToMoveTo(endX, endY, gridObject);
            return;
        }

        FinishMoving(endX, endY);
    }

    private void PerformMoveObject_Relative(int X, int Y)
    {
        PerformMoveObject_Absolute(gridObject.X + X, gridObject.Y + Y);
    }

    private void PerformMoveObject_RelativeDirectional(Direction direction)
    {
        Vector2 move = direction.GetUnitDirection() * step;
        PerformMoveObject_Relative((int)move.x, (int)move.y);
    }

    private void FinishMoving(int endX, int endY)
    {
        OnObjectPrearrivalCallback();
        gridObject.SetGridPosition(endX, endY);
        gridObject.ValidateAndAssertObjectPosition();
        OnObjectArrivalCallback();
    }

    private void OnObjectPrearrivalCallback()
    {
        foreach(IObjectPreArrivalCallback objectPreArrivalCallback in objectPreArrivalCallbacks)
        {
            objectPreArrivalCallback.ObjectPreArrived();
        }
    }

    private void OnObjectArrivalCallback()
    {
        
        foreach (IObjectArrivalCallback objectArrivalCallback in objectArrivalCallbacks)
        {
            objectArrivalCallback.ObjectArrived();
        }
    }

    /// <summary>
    /// Custom method that lerps a gridObject from its position to the end of a gridDirectionVector.
    /// 
    /// Meaning that if we're at 1, 1 and move by a direction vector of 0, 1 we'll end up at 1, 2 smoothly.
    /// LerpIncrement is fixed at the time of writing this doc.
    /// 
    /// </summary>
    private IEnumerator PerformMoveObjectLerp_Absolute(int X, int Y)
    {
        int endX = X;
        int endY = Y;

        if (!CanMoveTo(endX, endY))
        {
            failedToMoveToCallback.FailedToMoveTo(endX, endY, gridObject);
            yield break;
        }

        movementBlocked = true;

        Vector3 startPosition = gridObject.transform.position;
        
        Vector2 worldPositionEnd = GameGrid.INSTANCE.GridToWorldCoordinates(endX, endY);

        float lerpNow = 0f;


        // This is basically a FixedUpdate call done through a Coroutine
        while (true)
        {
            yield return new WaitForFixedUpdate();
            lerpNow += lerpIncrementor;
            lerpNow = Mathf.Clamp(lerpNow, 0f, 1f);
            gridObject.gameObject.transform.position = Vector3.Lerp(startPosition, worldPositionEnd, lerpNow);
            
            // Is nearly equal to 1 - meaning we've arrived at the destination.
            if(Mathf.Abs(1f - lerpNow) < 0.001f)
            {
                // When we've arrived finally update the gridPosition, note that it also applies Updating the position to the end
                // even though we've already arrived.
                FinishMoving(endX, endY);
                break;
            }

        }
        movementBlocked = false;
        yield break;



    }
    
    private IEnumerator PerformMoveObjectLerp_Relative(int X, int Y)
    {
        return PerformMoveObjectLerp_Absolute(gridObject.X + X, gridObject.Y + Y);
    }

    private IEnumerator PerformMoveObjectLerp_RelativeDirectional(Direction direction)
    {
        Vector2 move = direction.GetUnitDirection() * step;
        return PerformMoveObjectLerp_Relative((int)move.x, (int)move.y);
    }

}
