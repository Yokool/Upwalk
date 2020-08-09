using UnityEngine;

[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(Moveable))]
public class MoveTowardsPlayerMovementIntelligence : MonoBehaviour, IMovementIntelligence
{

    private Moveable moveable;
    private GridObject cachedPlayer;


    private void OnEnable()
    {
        moveable = GetComponent<Moveable>();
        cachedPlayer = PlayerScript.INSTANCE.gameObject.GetComponent<GridObject>();
    }

    public void MoveTile()
    {
        moveable.MoveObject_Towards_Simple(cachedPlayer);
    }
}