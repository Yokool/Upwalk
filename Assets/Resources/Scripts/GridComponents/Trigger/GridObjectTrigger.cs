using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
[DisallowMultipleComponent]
public class GridObjectTrigger : MonoBehaviour
{
    
    private GridObject gridObject;

    private ITriggerCallback triggerCallback;

    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();
        triggerCallback = GetComponent<ITriggerCallback>();
    }

    public void NotifyTrigger(GridObject triggered)
    {
        TriggerEvent triggerEvent = new TriggerEvent() {

            thisObject = gridObject,
            triggeringObject = triggered
        
        };

        triggerCallback.HandleTrigger(triggerEvent);

    }
}
