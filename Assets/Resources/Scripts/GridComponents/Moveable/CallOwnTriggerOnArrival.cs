using UnityEngine;

[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(GridObjectTrigger))]
public class CallOwnTriggerOnArrival : MonoBehaviour, IObjectArrivalCallback
{
    public void ObjectArrived(ArrivalInformation arrivalInformation)
    {
        GridObject thisGridObject = GetComponent<GridObject>();
        GridObjectTrigger thisTrigger = GetComponent<GridObjectTrigger>();
        foreach(GridObject gridObjectAt in GameGrid.INSTANCE.GridObjectsAtObjectFiltered(thisGridObject))
        {
            thisTrigger.NotifyTrigger(gridObjectAt);
        }
    }
}