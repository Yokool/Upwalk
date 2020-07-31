using UnityEngine;

[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(GridObjectTrigger))]
public class CallOwnTriggerOnArrival : MonoBehaviour, IObjectArrivalCallback
{
    public void ObjectArrived()
    {
        GridObject thisGridObject = GetComponent<GridObject>();
        GridObjectTrigger thisTrigger = GetComponent<GridObjectTrigger>();
        foreach(GridObject obj in GameGrid.INSTANCE.GridObjectsAtObjectFiltered(thisGridObject))
        {
            thisTrigger.NotifyTrigger(obj);
        }
        
    }
}