using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehaviour))]
public class TileMoverBase : MonoBehaviour, IOnNextTurn_Callback
{
    private EnemyBehaviour enemyBehaviour;

    private int currentIteration = 1;

    [SerializeField]
    private int maxIteration;

    public void OnNextTurn()
    {
        ++currentIteration;
        if(currentIteration > maxIteration)
        {
            currentIteration = 1;
        }
        MoveTiles();
    }

    private void MoveTiles()
    {
        foreach(GridObject tile in enemyBehaviour.GetEnemyTiles())
        {
            TileIterator iterator = tile.GetComponent<TileIterator>();
            if(iterator == null)
            {
                continue;
            }

            iterator.MoveTilesOnIteration(currentIteration);
        }
    }

    private void OnEnable()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
    }

}
