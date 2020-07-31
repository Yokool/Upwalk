using System.Collections.Generic;
using UnityEngine;

public static class TileTypeDataDatabase
{

    private static Dictionary<TileType, TileTypeData> tileTypeDatabase = new Dictionary<TileType, TileTypeData>();

    public static Dictionary<TileType, TileTypeData> TileTypeDatabase => tileTypeDatabase;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitSystem()
    {
        tileTypeDatabase = new Dictionary<TileType, TileTypeData>()
        {
            {TileType.GROUND, new TileTypeData(false, 0, true, true) },
            {TileType.ALIVE, new TileTypeData(false, 1, false, true) },
            {TileType.SPAWNER, new TileTypeData(true, 0, true, false) },
            {TileType.WALL, new TileTypeData(true, 2, false, true) },
            {TileType.CAMERA, new TileTypeData(false, 0, true, false) },
            {TileType.TRIGGER, new TileTypeData(true, 1, true, true) }
        };
    }




}
