using UnityEngine;
using System;
using System.Collections.Generic;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;
    public EnemyData EnemyData => enemyData;


    private List<GridObject> enemyTiles = new List<GridObject>();

    public GridObject[] GetEnemyTiles()
    {
        return enemyTiles.ToArray();
    }

    public void AddEnemyTile(GridObject enemyTile)
    {
        enemyTiles.Add(enemyTile);
    }

    public void RemoveEnemyTile(GridObject enemyTile)
    {
        enemyTiles.Remove(enemyTile);
    }


}
