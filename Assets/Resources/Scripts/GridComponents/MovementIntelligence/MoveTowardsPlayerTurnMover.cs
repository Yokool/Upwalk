using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(Moveable))]
public class MoveTowardsPlayerTurnMover : MonoBehaviour, ITurnMover
{
    private GridObject gridObject;
    private GridObject cachedPlayer;


    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();

        cachedPlayer = PlayerScript.INSTANCE.gameObject.GetComponent<GridObject>();
    }

    public Vector2Int GetTileToMoveTo()
    {

        int dX = cachedPlayer.X - gridObject.X;
        int dY = cachedPlayer.Y - gridObject.Y;

        dX = Mathf.Clamp(dX, -1, 1);
        dY = Mathf.Clamp(dY, -1, 1);

        Vector2Int endLoc = new Vector2Int(gridObject.X + dX, gridObject.Y + dY);

        return endLoc;

    }
}