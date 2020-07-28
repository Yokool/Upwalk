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