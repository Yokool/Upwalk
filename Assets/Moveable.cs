using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GridObject))]
public class Moveable : MonoBehaviour
{

    private GridObject gridObject = null;

    [SerializeField]
    private int step = 0;


    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();
    }


    public void MoveObject(Direction direction)
    {

        Vector2 unitVector = GetUnitDirection(direction);

        unitVector *= step;

        gridObject.X += (int)unitVector.x;
        gridObject.Y += (int)unitVector.y;

    }

    private Vector2 GetUnitDirection(Direction direction)
    {

        Vector2 unit = Vector2.zero;

        if (direction.HasFlag(Direction.NORTH))
        {
            unit.y += 1f; // Floats are used to prevent implicit casting since unity vectors use floats
        }
        else if (direction.HasFlag(Direction.SOUTH))
        {
            unit.y -= 1f;
        }

        if (direction.HasFlag(Direction.EAST))
        {
            unit.x += 1f;
        }
        else if (direction.HasFlag(Direction.WEST))
        {
            unit.x -= 1f;
        }


        return unit;



    }


}
