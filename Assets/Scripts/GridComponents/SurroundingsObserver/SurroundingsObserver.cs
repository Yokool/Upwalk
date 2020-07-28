using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(ISurroundingsChangedCallback))]
public class SurroundingsObserver : MonoBehaviour
{

    private ISurroundingsChangedCallback surroundingsChangedCallback;

    private void OnEnable()
    {
        surroundingsChangedCallback = GetComponent<ISurroundingsChangedCallback>();
    }

    public void SurroundingsChanged(GridObject observedObject)
    {
        surroundingsChangedCallback.SurroundingsChanged(observedObject);
    }

}
