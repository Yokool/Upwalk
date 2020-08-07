using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Direction bit flag specifies a direction. Complex direction like NORTH EAST are combined through the
/// use assigning multiple flags.
/// 
/// IE: direction = Direction.NORTH | Direction.EAST
/// 
/// If you would really really want to add a new direction which you won't - don't forget to update DirectionExtension.IsEmpty.
/// 
/// </summary>
[Flags]
public enum Direction
{
    NONE = 0,
    NORTH = 1 << 0,
    SOUTH = 1 << 1,
    EAST = 1 << 2,
    WEST = 1 << 3


}


public static class DirectionExtension
{
    /// <summary>
    /// Determines if the Direction bit flag enum contains only Direction.NONE.
    /// Direction.NONE means no direction.
    /// You can use this method if you want to check if a the bit flag enum has SOME direction to perform certain logic.
    /// </summary>
    public static bool IsEmpty(this Direction direction)
    {
        return (direction & (Direction.NORTH | Direction.SOUTH | Direction.EAST | Direction.WEST)) == Direction.NONE;
    }

    public static Vector2 GetUnitDirection(this Direction direction)
    {

        Vector2 unitVector = Vector2.zero;

        if (direction.HasFlag(Direction.NORTH))
        {
            unitVector.y += 1f; // Floats are used to prevent implicit casting since unity vectors use floats
        }
        else if (direction.HasFlag(Direction.SOUTH))
        {
            unitVector.y -= 1f;
        }

        if (direction.HasFlag(Direction.EAST))
        {
            unitVector.x += 1f;
        }
        else if (direction.HasFlag(Direction.WEST))
        {
            unitVector.x -= 1f;
        }


        return unitVector;



    }

    public static IEnumerable<Direction> EnumerateOverFlags(this Direction direction)
    {
        foreach(Enum value in Enum.GetValues(typeof(Direction)))
        {

            if (direction.HasFlag(value))
            {
                yield return (Direction)value;
            }

        }
    }

    public static Direction GetDirectionFromVector(int X, int Y)
    {

        Direction dir = Direction.NONE;

        if (X >= 1)
        {
            dir = dir | Direction.EAST;
        }
        else if (X <= -1)
        {
            dir = dir | Direction.WEST;
        }


        if(Y >= 1)
        {
            dir = dir | Direction.NORTH;
        }
        else if(Y <= -1)
        {
            dir = dir | Direction.SOUTH;
        }

        return dir;

    }

}