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


    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();
    }

    public void MoveObject(Direction direction)
    {
        if (movementBlocked)
        {
            return;
        }

        // This represents the end position in GRID coordinates
        Vector2 directionVector = direction.GetUnitDirection();

        directionVector *= step;

        
        if (shouldLerp)
        {
            // Convert the grid coordinates into WORLD position
            StartCoroutine(LerpPosition(gridObject, directionVector));
            return;
        }

        int endX = gridObject.X + (int)directionVector.x;
        int endY = gridObject.Y + (int)directionVector.y;

        if(CanMoveTo(endX, endY))
        {
            gridObject.SetGridPosition(endX, endY);
            gridObject.ValidateObjectPosition();
        }

    }

    public bool CanMoveTo(int X, int Y)
    {
        // Can Go through solids or
        // Filter to all the ones we can't walk through and if there is one say no
        return canGoThroughSolids || GameGrid.INSTANCE.ObjectsAt(X, Y).Where((gridObj) =>{
            return !TileTypeDataDatabase.TileTypeDatabase[gridObj.m_TileType].walkthrough;
        }).ToArray().Length == 0;
    }

    
    /// <summary>
    /// Custom method that lerps a gridObject from its position to the end of a gridDirectionVector.
    /// 
    /// Meaning that if we're at 1, 1 and move by a direction vector of 0, 1 we'll end up at 1, 2 smoothly.
    /// LerpIncrement is fixed at the time of writing this doc.
    /// 
    /// </summary>
    private IEnumerator LerpPosition(GridObject objToLerp, Vector2 gridDirectionVector)
    {

        int endX = gridObject.X + (int)gridDirectionVector.x;
        int endY = gridObject.Y + (int)gridDirectionVector.y;

        if (!CanMoveTo(endX, endY))
        {
            yield break;
        }

        movementBlocked = true;
        Vector3 startPosition = objToLerp.transform.position;
        
        // We have to walk from our gridPosition to the end of the gridDirectionVector and convert it to WorldPos
        Vector2 worldPositionEnd = GameGrid.INSTANCE.GridToWorldCoordinates((int)gridDirectionVector.x + objToLerp.X, (int)gridDirectionVector.y + objToLerp.Y);

        float lerpNow = 0f;

        

        // This is basically a FixedUpdate call done through a Coroutine
        while (true)
        {
            yield return new WaitForFixedUpdate();

            lerpNow += lerpIncrementor;
            lerpNow = Mathf.Clamp(lerpNow, 0f, 1f);
            objToLerp.gameObject.transform.position = Vector3.Lerp(startPosition, worldPositionEnd, lerpNow);
            
            // Is nearly equal to 1 - meaning we've arrived at the destination.
            if(Mathf.Abs(1f - lerpNow) < 0.001f)
            {
                // When we've arrived finally update the gridPosition, note that it also applies Updating the position to the end
                // even though we've already arrived.

                gridObject.SetGridPosition(endX, endY);
                gridObject.ValidateObjectPosition();

                break;
            }

        }

        movementBlocked = false;
        yield break;



    }
    
}
