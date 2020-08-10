﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehaviour))]
[DisallowMultipleComponent]
public class SubTileMoverParent : MonoBehaviour, IOnNextTurn_Callback
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
        MoveChildTiles();
    }

    private void MoveChildTiles()
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

            SubTileMover iterator = tile.GetComponent<SubTileMover>();
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