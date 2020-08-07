using UnityEngine;

public class MoveChildrenTooPreArrivalCallback : MonoBehaviour, IObjectPreArrivalCallback
{
    public void ObjectPreArrived(ArrivalInformation arrivalInformation)
    {
        Transform travellingObjectTransform = arrivalInformation.TravellingObject.transform;
        GridObject travellingObjectGridObject = arrivalInformation.TravellingObject.GetComponent<GridObject>();
        

        if(travellingObjectGridObject == null)
        {
            Debug.LogError($"Class: {nameof(MoveChildrenTooPreArrivalCallback)}, Method: {nameof(ObjectPreArrived)} whose travelling object {arrivalInformation.TravellingObject} contains no {nameof(GridObject)} component.");
            return;
        }

        for (int i = 0; i < travellingObjectTransform.childCount; ++i)
        {
            Transform child = travellingObjectTransform.GetChild(i);

            GridObject childGridObject = child.GetComponent<GridObject>();
            Moveable childMoveable = child.GetComponent<Moveable>();

            if(childGridObject == null || childMoveable == null)
            {
                Debug.LogError($"Class: {nameof(MoveChildrenTooPreArrivalCallback)}, Method: {nameof(ObjectPreArrived)} was called on an object {gameObject} whose child {child} contains no {nameof(GridObject)} component or {nameof(Moveable)} component.");
                continue;
            }

            int dX = -travellingObjectGridObject.X + arrivalInformation.TargetX;
            int dY = -travellingObjectGridObject.Y + arrivalInformation.TargetY;

            Direction direction = DirectionExtension.GetDirectionFromVector(dX, dY);

            childMoveable.MoveObject_RelativeDirectional(direction);

        }
    }
}