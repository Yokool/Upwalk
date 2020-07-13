using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
public class GridObjectTrigger : MonoBehaviour
{
    
    private GridObject gridObject;

    [SerializeField]
    private TriggerCallback triggerCallback;

    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();
    }

    public void NotifyTrigger(GridObject triggered)
    {
        TriggerEvent triggerEvent = new TriggerEvent() {

            thisObject = gridObject,
            trigerringObject = triggered
        
        };

        triggerCallback.HandleTrigger(triggerEvent);


    }
}


public interface TriggerCallback
{
    void HandleTrigger(TriggerEvent message);
}

public struct TriggerEvent
{
    public GridObject thisObject;
    public GridObject trigerringObject;
}
