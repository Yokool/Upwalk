using System;
using System.Collections;

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
