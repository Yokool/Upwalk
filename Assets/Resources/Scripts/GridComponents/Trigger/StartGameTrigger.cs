using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameTrigger : MonoBehaviour, ITriggerCallback
{
    public void HandleTrigger(TriggerEvent message)
    {
        if (GameLifetimeManager.INSTANCE.GameStarted)
        {
            return;
        }

        GameLifetimeManager.INSTANCE.StartGame();
    }
}
