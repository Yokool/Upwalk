using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISurroundingsChangedCallback
{
    void SurroundingsChanged(GridObject observedObject);
}
