using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Moveable))]
[RequireComponent(typeof(OnNextTurnCallback_Base))]
public class GridObjectIntelligence : MonoBehaviour, IOnNextTurn_Callback
{

    private IMovementIntelligence[] intelligences;
    private IMovementIntelligenceCheck[] intelligenceChecks;

    private void OnEnable()
    {
        intelligences = GetComponents<IMovementIntelligence>();
        intelligenceChecks = GetComponents<IMovementIntelligenceCheck>();
    }

    public void OnNextTurn()
    {

        bool checkSuccess = false;

        foreach(IMovementIntelligenceCheck intelligenceCheck in intelligenceChecks)
        {
            bool cache = intelligenceCheck.PerformCheck();
            // Go through ALL the checks, even if we fail on the first one
            // we have to check every single one
            checkSuccess = checkSuccess || cache;
        }

        if (!checkSuccess)
        {
            return;
        }

        foreach(IMovementIntelligence intelligence in intelligences)
        {
            intelligence.MoveTile();
        }
        
    }
}
