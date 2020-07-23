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


    private List<GridObject> rowRandomSpawners = new List<GridObject>();

    [SerializeField]
    private GameObject spawnObject = null;

    /// <summary>
    /// Adds a GridObject entry inside the GameGrid system, which enables such object to be acknowledged by the GameGrid methods.
    /// </summary>
    /// <param name="gridObject"></param>
    public void AddEntry(GridObject gridObject)
    {
        gridObjects.AddLast(gridObject);
        NotifyObservers(gridObject);
    }

    /// <summary>
    /// Triggers all GridObjectTriggers on the position of the gridObject in the parameter that are not the parameter itself.
    /// </summary>
    /// <param name="gridObject"></param>
    public void NotifyTriggers(GridObject gridObject)
    {
        GridObject[] objects = GridObjectsAtObjectFiltered(gridObject);
        foreach (GridObject objectAt in objects)
        {
            // Check if the object wasn't destroyed after we've gathered the objects.
            if (objectAt == null)
            {
                Debug.LogWarning("NotifyTriggers caught an edge case: GridObjectsAtObjectFiltered contained a null reference, it's possible that the object got destroyed in the meantime.");
                RemoveEntry(objectAt);
                continue;
            }

            GridObjectTrigger trigger;
            objectAt.TryGetComponent<GridObjectTrigger>(out trigger);

            if(trigger != null)
            {
                trigger.NotifyTrigger(gridObject);
            }


        }

    }

    public void NotifyObservers(GridObject gridObject)
    {
        GridObject[] surroundingObjects = GridObjectsAtRelativeSquare(gridObject);

        foreach(GridObject surroundingObject in surroundingObjects)
        {
            if(surroundingObject == null)
            {
                Debug.LogWarning("NotifyObservers caught an edge case: gridObjectsAtPosition contained a null object. Possible that it was destroyed in the meantime.");
                continue;
            }


            SurroundingsObserver observer = surroundingObject.GetComponent<SurroundingsObserver>();

            if(observer != null)
            {
                observer.SurroundingsChanged(gridObject);
            }

        }


    }
    
    /// <summary>
    /// Checks for illegal duplicates that are on the same tile and destroys them in the benefit of the parameter object, essentially replacing
    /// them.
    /// </summary>
    /// <param name="gridObject"></param>
    public void CheckForAndHandleIllegalOverlaps(GridObject gridObject)
    {

        TileTypeData data = TileTypeDataDatabase.TileTypeDatabase[gridObject.m_TileType];
        
        if (data.canExistMultiple)
        {
            return;
        }

        GridObject[] gridObjectsAtPosition = GridObjectsAtObjectFiltered(gridObject);

        foreach(GridObject gridObjectAtPosition in gridObjectsAtPosition)
        {

            if (gridObjectAtPosition == null)
            {
                Debug.LogWarning("CheckForAndHandleIllegalOverlaps caught an edge case: gridObjectsAtPosition contained a null object. Possible that it was destroyed in the meantime.");
                continue;
            }

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
    public GridObject[] GridObjectsAtObjectFiltered(GridObject gridObject)
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

    public GridObject[] ObjectsAtRelative(GridObject gridObject, int X, int Y)
    {
        return ObjectsAt(gridObject.X + X, gridObject.Y + Y);
    }

    public GridObject[] ObjectsAtRelativeDirectional(GridObject gridObject, Direction direction)
    {
        if (direction.IsEmpty())
        {
            return new GridObject[0];
        }
        
        
        Vector2 gridVector = Vector2.zero;

        foreach(Direction dir in direction.EnumerateOverFlags())
        {
            gridVector += dir.GetUnitDirection();
        }

        return ObjectsAtRelative(gridObject, (int)gridVector.x, (int)gridVector.y);

    }
    /// <summary>
    /// Returns all gridObjects in a square around the parameter gridObject.
    /// 
    /// <para>The method does not return objects under the parameter.</para>
    /// </summary>
    /// <param name="gridObject"></param>
    /// <returns></returns>
    public GridObject[] GridObjectsAtRelativeSquare(GridObject gridObject)
    {
        // This is so unbelievably expensive, you're looping over the same list 8 times
        // you could just gather a list of the coordinates and optimize it
        GridObject[] N = ObjectsAtRelativeDirectional(gridObject, Direction.NORTH);
        GridObject[] S = ObjectsAtRelativeDirectional(gridObject, Direction.SOUTH);
        GridObject[] E = ObjectsAtRelativeDirectional(gridObject, Direction.EAST);
        GridObject[] W = ObjectsAtRelativeDirectional(gridObject, Direction.WEST);

        GridObject[] NE = ObjectsAtRelativeDirectional(gridObject, Direction.NORTH | Direction.EAST);
        GridObject[] NW = ObjectsAtRelativeDirectional(gridObject, Direction.NORTH | Direction.WEST);

        GridObject[] SE = ObjectsAtRelativeDirectional(gridObject, Direction.SOUTH | Direction.EAST);
        GridObject[] SW = ObjectsAtRelativeDirectional(gridObject, Direction.SOUTH | Direction.WEST);

        List<GridObject> result = new List<GridObject>();

        return result.Concat(N).Concat(S).Concat(E).Concat(W).Concat(NE).Concat(NW).Concat(SE).Concat(SW).ToArray();

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

    public void AddRandomRowEntry(GridObject gridObject)
    {
        if(gridObject.GetComponent<GridObjectSpawnerRandomExtension>() == null)
        {
            Debug.LogError($"{nameof(AddRandomRowEntry)} was called with the parameter {gridObject} which does not contain the component {nameof(GridObjectSpawnerRandomExtension)}. Returning and not adding an entry.");
            return;
        }

        rowRandomSpawners.Add(gridObject);
    }

    public void RemoveRandomRowEntry(GridObject gridObject)
    {
        rowRandomSpawners.Remove(gridObject);
    }

    public void SpawnRandomRow()
    {
        // In order to avoid infinite recursion, we have to copy the list in order to not call the new entry
        List<GridObject> copy = rowRandomSpawners.ToList();

        foreach(GridObject obj in copy)
        {
            obj.GetComponent<GridObjectSpawnerRandomExtension>().SpawnRandomObjects();
        }

    }


}
