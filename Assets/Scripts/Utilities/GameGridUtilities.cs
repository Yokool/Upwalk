using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Utilities for special scenarios where the GameGrid object
/// in of itself would not work on its own.
/// </summary>
public class GameGridUtilities
{

    public Queue<GridObject> objectQueue = new Queue<GridObject>();

    private static GameGridUtilities instance;
    public static GameGridUtilities INSTANCE
    {
        get
        {

            if (instance == null)
            {
                instance = new GameGridUtilities();
            }

            return instance;

        }
    }
    /// <summary>
    /// Adds a GridObject into the GameGrid when it has
    /// not been yet Awaked. This is called from OnEnable
    /// callbacks in GridObject since there is no uniform
    /// way of Awaking/Start/OnEnable call order.
    /// 
    /// The GridObjects are enqued until the singleton
    /// becomes available. Once it does. They are processed
    /// one by one.
    /// 
    /// This method should not be called normally. Only in
    /// setup scenarios, that is when the singleton
    /// is not available / not initialized.
    /// If you want to add your grid
    /// object into the system. Use the GameGrid.AddEntry
    /// method.
    /// 
    /// </summary>
    /// <param name="gridObject"></param>
    public void AddGridObjectOnStartup(GridObject gridObject)
    {
        GameGrid gameGrid = GameGrid.INSTANCE;
        if (gameGrid == null)
        {
            objectQueue.Enqueue(gridObject);
            return;
        }

        gameGrid.AddEntry(gridObject);


    }


}