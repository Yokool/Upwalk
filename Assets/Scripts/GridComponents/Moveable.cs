using System.Collections;
using System.Collections.Generic;
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


        gridObject.SetGridPosition(gridObject.X + (int)directionVector.x, gridObject.Y + (int)directionVector.y);
        gridObject.ValidateObjectPosition();

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
        movementBlocked = true;
        Vector3 startPosition = objToLerp.transform.position;
        
        // We have to walk from our gridPosition to the end of the gridDirectionVector and convert it to WorldPos
        Vector2 worldPositionEnd = GameGrid.INSTANCE.GridToWorldCoordinates((int)gridDirectionVector.x + objToLerp.X, (int)gridDirectionVector.y + objToLerp.Y);

        float lerpIncrement = 0.1f;
        float lerpNow = 0f;

        // This is basically a FixedUpdate call done through a Coroutine
        while (true)
        {
            yield return new WaitForFixedUpdate();

            lerpNow += lerpIncrement;
            lerpNow = Mathf.Clamp(lerpNow, 0f, 1f);
            objToLerp.gameObject.transform.position = Vector3.Lerp(startPosition, worldPositionEnd, lerpNow);
            
            // Is nearly equal to 1 - meaning we've arrived at the destination.
            if(Mathf.Abs(1f - lerpNow) < 0.001f)
            {
                // When we've arrived finally update the gridPosition, note that it also applies Updating the position to the end
                // even though we've already arrived.

                gridObject.SetGridPosition(gridObject.X + (int)gridDirectionVector.x, gridObject.Y + (int)gridDirectionVector.y);
                gridObject.ValidateObjectPosition();

                break;
            }

        }

        movementBlocked = false;
        yield break;



    }
    
}
