using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGameTrigger : MonoBehaviour, ITriggerCallback
{
    public void HandleTrigger(TriggerEvent message)
    {
        GameLifetimeManager.INSTANCE.ExitGame();
    }

}
