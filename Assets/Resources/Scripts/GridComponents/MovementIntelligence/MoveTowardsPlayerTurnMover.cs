using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(Moveable))]
public class MoveTowardsPlayerTurnMover : MonoBehaviour, ITurnMover
{
    private GridObject gridObject;
    private GridObject cachedPlayer;
    private Moveable moveable;

    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();
        moveable = GetComponent<Moveable>();

        cachedPlayer = PlayerScript.INSTANCE.gameObject.GetComponent<GridObject>();
    }

    public void GetTileToMoveTo()
    {
        moveable.MoveObject_Towards_Simple(cachedPlayer);
    }
}