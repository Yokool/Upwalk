using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(GridObject))]
[DisallowMultipleComponent]
public class Moveable : MonoBehaviour
{
    /* 
     * LEGACY ASYNC CODE
     * 
    public const int NO_MOVEMENT_CODE = -101011;
    */

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
    private IFailedToMoveToCallback[] failedToMoveToCallbacks;
    private IOnObjectMovementStart[] onObjectMovementStarts;

    /*
     * LEGACY ASYNC CODE
     * 
     * 
    private int asyncEndX;
    private int asyncEndY;
    */
    

    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();
        objectArrivalCallbacks = GetComponents<IObjectArrivalCallback>();
        failedToMoveToCallbacks = GetComponents<IFailedToMoveToCallback>();
        onObjectMovementStarts = GetComponents<IOnObjectMovementStart>();
    }

    public void MoveObject_Towards_Simple(GridObject targetObject)
    {

        int dX = targetObject.X - gridObject.X;
        int dY = targetObject.Y - gridObject.Y;

        Direction direction = DirectionExtension.GetDirectionFromVector(dX, dY);


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

        // Can Go through solids or
        // Filter to all the ones we can't walk through and if there is one say no
        bool canMoveTo = canGoThroughSolids || objectsAt.Where((gridObj) => {
            return !TileTypeDataDatabase.TileTypeDatabase[gridObj.m_TileType].walkthrough;
        }).ToArray().Length == 0;

        return canMoveTo;
    }

    #region PERFORM_NON_LERP_MOVEMENT

    private void PerformMoveObject_Absolute(int endX, int endY)
    {
        if (!CanMoveTo(endX, endY))
        {
            FailedToMoveToCallbackInvoke(endX, endY, gridObject);
            return;
        }
        
        OnObjectStartedMoving(endX, endY, false);
        OnObjectEndedMoving(endX, endY);
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

    #endregion

    #region PERFORM_LERP_MOVEMENT

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
            FailedToMoveToCallbackInvoke(endX, endY, gridObject);
            yield break;
        }

        

        movementBlocked = true;

        Vector3 startPosition = gridObject.transform.position;
        
        Vector2 worldPositionEnd = GameGrid.INSTANCE.GridToWorldCoordinates(endX, endY);

        float lerpNow = 0f;

        OnObjectStartedMoving(endX, endY, true);

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
                OnObjectEndedMoving(endX, endY);
                
                break;
            }

        }


        gridObject.UpdatePositionToGrid();

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

    #endregion

    #region CALLBACK_INVOKES

    public void FailedToMoveToCallbackInvoke(int endX, int endY, GridObject gridObject)
    {
        foreach (IFailedToMoveToCallback failedToMoveToCallback in failedToMoveToCallbacks)
        {
            failedToMoveToCallback.FailedToMoveTo(endX, endY, gridObject);
        }
    }

    private void OnObjectStartedMovingCallbackInvoke(int dX, int dY)
    {
        ArrivalInformation arrivalInformation = new ArrivalInformation(gameObject, dX, dY);
        foreach (IOnObjectMovementStart onObjectMovementStart in onObjectMovementStarts)
        {
            onObjectMovementStart.ObjectStartedMoving(arrivalInformation);
        }
    }

    private void OnObjectArrivalCallbackInvoke(int dX, int dY)
    {
        ArrivalInformation arrivalInformation = new ArrivalInformation(gameObject, dX, dY);
        foreach (IObjectArrivalCallback objectArrivalCallback in objectArrivalCallbacks)
        {
            objectArrivalCallback.ObjectArrived(arrivalInformation);
        }
    }

    #endregion

    private void OnObjectStartedMoving(int endX, int endY, bool dontUpdatePosition)
    {
        int currX = gridObject.X;
        int currY = gridObject.Y;
        gridObject.SetGridPosition(endX, endY, dontUpdatePosition);
        gridObject.ValidateAndAssertObjectPosition();
        OnObjectStartedMovingCallbackInvoke(endX - currX, endY - currY);
    }

    private void OnObjectEndedMoving(int endX, int endY)
    {
        int currX = gridObject.X;
        int currY = gridObject.Y;
        OnObjectArrivalCallbackInvoke(endX - currX, endY - currY);
    }

}
