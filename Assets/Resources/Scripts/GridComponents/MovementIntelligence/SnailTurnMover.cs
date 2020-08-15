using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(Moveable))]
[RequireComponent(typeof(GridObjectTurnMovement))]
public class SnailTurnMover : MonoBehaviour, ITurnMover
{
    private GridObject gridObject;
    private Moveable moveable;

    private Direction directionToMove = Direction.WEST;

    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();
        moveable = GetComponent<Moveable>();
    }


    public void GetTileToMoveTo()
    {
        GridObject[] objectsInTheNextTile = GameGrid.INSTANCE.ObjectsAtRelativeDirectional(gridObject, directionToMove);

        bool canMoveTo = true;

        for(int i = 0; i < objectsInTheNextTile.Length; ++i)
        {
            if (!TileTypeDataDatabase.TileTypeDatabase[objectsInTheNextTile[i].m_TileType].walkthrough)
            {
                
                canMoveTo = false;
                break;
            }
        }

        if (!canMoveTo)
        {
            directionToMove = directionToMove.GetOppositeDirection();
        }

        moveable.MoveObject_RelativeDirectional(directionToMove);

    }
}