using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum Direction
{
    NONE = 0,
    NORTH = 1 << 0,
    SOUTH = 1 << 1,
    EAST = 1 << 2,
    WEST = 1 << 3,
}
