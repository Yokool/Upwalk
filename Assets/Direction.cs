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

}