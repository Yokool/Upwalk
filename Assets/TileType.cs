using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    
    GROUND,
    PLAYER

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
            {TileType.GROUND, new TileTypeData(false) },
            {TileType.PLAYER, new TileTypeData(false) }
        };
    }




}

public struct TileTypeData
{
    /// <summary>
    /// Determines if multiple tiles of the same time can be in the same location.
    /// </summary>
    public bool canExistMultiple;
    


    public TileTypeData(bool canExistMultiple)
    {
        this.canExistMultiple = canExistMultiple;
    }

}