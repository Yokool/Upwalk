using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTurnOnArrival : MonoBehaviour, IOnObjectMovementStart
{
    public void ObjectStartedMoving(ArrivalInformation arrivalInformation)
    {
        TurnSystem.INSTANCE.NextTurn();
    }
}
