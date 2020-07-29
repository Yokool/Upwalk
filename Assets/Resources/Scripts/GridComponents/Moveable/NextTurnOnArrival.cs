using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTurnOnArrival : MonoBehaviour, IObjectPreArrivalCallback
{
    public void ObjectPreArrived()
    {
        TurnSystem.INSTANCE.NextTurn();
    }
}
