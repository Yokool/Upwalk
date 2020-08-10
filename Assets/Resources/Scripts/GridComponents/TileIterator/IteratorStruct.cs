using UnityEngine;

[System.Serializable]
public struct SubTileMoverStruct
{
    [Range(1, 10000)]
    public int interationTurn;
    public int X;
    public int Y;
}
