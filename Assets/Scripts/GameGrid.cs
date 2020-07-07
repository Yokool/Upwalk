using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{

    [SerializeField]
    private float gridX = 0f;

    public float GridX => gridX;

    [SerializeField]
    private float gridY = 0f;


    public float GridY => gridY;


    private static GameGrid instance;
    public static GameGrid INSTANCE => instance;


    private LinkedList<GridObject> gridObjects = new LinkedList<GridObject>();
    
    public void AddEntry(GridObject gridObject)
    {
        gridObjects.AddLast(gridObject);
    }

    public void RemoveEntry(GridObject gridObject)
    {
        gridObjects.Remove(gridObject);
    }

    public GridObject ObjectAt(int x, int y)
    {

        foreach(GridObject obj in gridObjects)
        {
            if(obj.X == x && obj.Y == y)
            {
                return obj;
            }

        }

        Debug.LogWarning($"{this}.ObjectAt(int {x}, int {y}) could not find such object that it would satisfy that criteria");
        return null;

    }


    void Awake()
    {
        if(INSTANCE != null)
        {
            Destroy(gameObject);
            Debug.LogWarning($"Tried to create a second GameGrid script. Destroying game object {gameObject}.");
        }
        instance = this;
    }


}


