using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTurnOnArrival : MonoBehaviour, IObjectArrivalCallback
{
    public void ObjectArrived()
    {
        TurnSystem.INSTANCE.NextTurn();
    }
}
