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
            {TileType.GROUND, new TileTypeData(false, 0) },
            {TileType.PLAYER, new TileTypeData(false, 1) },
            {TileType.SPAWN, new TileTypeData(true, 0) },
            {TileType.WALL, new TileTypeData(true, 1) }
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

    public TileTypeData(bool canExistMultiple, int spriteOrder)
    {
        this.canExistMultiple = canExistMultiple;
        this.spriteOrder = spriteOrder;
    }

}