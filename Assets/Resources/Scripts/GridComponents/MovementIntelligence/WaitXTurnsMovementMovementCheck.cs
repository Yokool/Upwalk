using UnityEngine;

public class WaitXTurnsMovementMovementCheck : MonoBehaviour, ITurnMovementCheck
{
    [SerializeField]
    private int turnToHit;

    private int turn = 0;

    public bool PerformCheck()
    {
        ++turn;

        bool success = turn == turnToHit;

        if (success)
        {
            turn = 0;
        }

        return success;

    }
}