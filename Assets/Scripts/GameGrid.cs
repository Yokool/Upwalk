using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    /// <summary>
    /// A list containing all GridObjects that should be handled in game.
    /// If you've got a gridObject that is disabled, from the OnDisable callback - such gridObject can't be handled by methods
    /// in this gameGrid class. For correct functionality on your gameObject, you must include an entry inside this list.
    /// 
    /// <para>---</para>
    /// 
    /// <para>Examples of grid functionality include:
    /// <list type="bullet">ObjectsAt type functions</list>
    /// <list type="bullet">Notifying triggers</list>
    /// <list type="bullet">CheckForAndHandleIllegalOverlaps</list>
    /// </para>
    /// 
    /// <para>---</para>
    /// 
    /// </summary>
    private LinkedList<GridObject> gridObjects = new LinkedList<GridObject>();


    [SerializeField]
    private GameObject spawnObject = null;


    public void AddEntry(GridObject gridObject)
    {
        gridObjects.AddLast(gridObject);

        CheckForAndHandleIllegalOverlaps(gridObject);
        NotifyTriggers(gridObject);
    }

    public void NotifyTriggers(GridObject gridObject)
    {
        foreach(GridObject objectAt in ObjectsAtGridObjectFiltered(gridObject))
        {

            GridObjectTrigger trigger;
            objectAt.TryGetComponent<GridObjectTrigger>(out trigger);

            if(trigger != null)
            {
                trigger.NotifyTrigger(gridObject);
            }


        }

    }
    
    public void CheckForAndHandleIllegalOverlaps(GridObject gridObject)
    {

        TileTypeData data = TileTypeDataDatabase.TileTypeDatabase[gridObject.m_TileType];
        
        if (data.canExistMultiple)
        {
            return;
        }

        GridObject[] gridObjectsAtPosition = ObjectsAtGridObjectFiltered(gridObject);

        foreach(GridObject gridObjectAtPosition in gridObjectsAtPosition)
        {
            if(gridObjectAtPosition.m_TileType.Equals(gridObject.m_TileType))
            {
                Destroy(gridObjectAtPosition.gameObject);
            }

        }

    }

    /// <summary>
    /// Removes a GridObject entry from the gridObjects list.
    /// </summary>
    /// <param name="gridObject"></param>
    public void RemoveEntry(GridObject gridObject)
    {
        gridObjects.Remove(gridObject);
    }

    /// <summary>
    /// Returns an array of gridObjects at the grid position x and y taken from the parameters.
    /// </summary>
    public GridObject[] ObjectsAt(int x, int y)
    {
        List<GridObject> objects = new List<GridObject>();

        foreach(GridObject obj in gridObjects)
        {
            if(obj.X == x && obj.Y == y)
            {
                objects.Add(obj);
            }

        }

        return objects.ToArray();

    }
    /// <summary>
    /// Returns an array of all the objects at the position of the parameter gridObject that are NOT the gridObject in the parameter.
    /// </summary>
    public GridObject[] ObjectsAtGridObjectFiltered(GridObject gridObject)
    {
        GridObject[] objects = ObjectsAtGridObjectNoFilter(gridObject);

        // Filter out the object in the parameter
        objects = objects.Where((obj) => { return obj != gridObject; }).ToArray();

        return objects;
    }

    /// <summary>
    /// Returns an array of all the objects at the position of the parameter gridObject, including the gridObject itself.
    /// </summary>
    public GridObject[] ObjectsAtGridObjectNoFilter(GridObject gridObject)
    {
        GridObject[] objects = ObjectsAt(gridObject.X, gridObject.Y);
        return objects;
    }

    /// <summary>
    /// Transforms the Grid coordinates into world coordinates returned as a Vector2.
    /// </summary>
    public Vector2 GridToWorldCoordinates(int X, int Y)
    {
        Vector2 returnVector = Vector2.zero;
        returnVector.x = (X * GridX);
        returnVector.y = (Y * GridY);

        return returnVector;

    }


    private void BuildSpawn()
    {
        GameObject spawnObj = Instantiate(spawnObject);
        // The spawn does not built itself on creation.
        spawnObj.GetComponent<GridStructureBehaviour>().BuildItself();
    }


    void Awake()
    {
        StandartSingletonInitialization();

        ProcessEnqueuedObjects();

        BuildSpawn();
    }

    private void StandartSingletonInitialization()
    {
        if (INSTANCE != null)
        {
            Destroy(gameObject);
            Debug.LogWarning($"Tried to create a second GameGrid script. Destroying game object {gameObject}.");
        }
        instance = this;
    }

    private void ProcessEnqueuedObjects()
    {
        Queue<GridObject> queue = GameGridUtilities.INSTANCE.objectQueue;
        foreach (GridObject obj in queue.ToList())
        {

            AddEntry(obj);
            queue.Dequeue();

        };
    }


}
