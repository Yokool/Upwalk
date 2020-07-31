using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehaviour))]
public class TileMoverBase : MonoBehaviour, IOnNextTurn_Callback
{
    private EnemyBehaviour enemyBehaviour;

    private int currentIteration = 0;

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
        GridObject[] enemyTiles = enemyBehaviour.GetEnemyTiles();
        for (int i = 0; i < enemyTiles.Length; i++)
        {
            GridObject tile = enemyTiles[i];

            // Cleanup might remove the tile before we are removed.
            if(tile == null)
            {
                continue;
            }

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
