using UnityEngine;

public class ObjectNLocation
{

    private GridObject gridObject;
    public GridObject m_GridObject => gridObject;


    private Vector2Int position;
    public Vector2Int Position => position;


    public ObjectNLocation(GridObject gridObject, Vector2Int position)
    {
        this.gridObject = gridObject;
        this.position = position;
    }

}
