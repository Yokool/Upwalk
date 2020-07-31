using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridObjectUtility
{

    public static int Distance(GridObject gridObjectOne, GridObject gridObjectTwo)
    {

        return Math.Max(gridObjectTwo.X - gridObjectOne.X, gridObjectOne.Y - gridObjectTwo.Y);

    }

}
