using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    
    GROUND,
    PLAYER,
    SPAWN,
    WALL

}


public static class TileTypeDataDatabase
{

    private static Dictionary<TileType, TileTypeData> tileTypeDatabase = new Dictionary<TileType, TileTypeData>();

    public static Dictionary<TileType, TileTypeData> TileTypeDatabase => tileTypeDatabase;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitSystem()
    {
        tileTypeDatabase = new Dictionary<TileType, TileTypeData>()
        {
            {TileType.GROUND, new TileTypeData(false, 0, true) },
            {TileType.PLAYER, new TileTypeData(false, 1, false) },
            {TileType.SPAWN, new TileTypeData(true, 0, true) },
            {TileType.WALL, new TileTypeData(true, 1, false) }
        };
    }




}

public struct TileTypeData
{
    /// <summary>
    /// Determines if multiple tiles of the same time can be in the same location.
    /// </summary>
    public bool canExistMultiple;

    public int spriteOrder;

    /// <summary>
    /// Determines if you can walk through the tile.
    /// </summary>
    public bool walkthrough;

    public TileTypeData(bool canExistMultiple, int spriteOrder, bool walkthrough)
    {
        this.canExistMultiple = canExistMultiple;
        this.spriteOrder = spriteOrder;
        this.walkthrough = walkthrough;
    }

}