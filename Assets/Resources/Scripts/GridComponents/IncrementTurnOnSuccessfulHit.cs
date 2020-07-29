using UnityEngine;

public class IncrementTurnOnSuccessfulHit : MonoBehaviour, IOnSuccessfulHitCallback
{
    public void OnSuccessfulHit()
    {
        TurnSystem.INSTANCE.NextTurn();
    }
}
