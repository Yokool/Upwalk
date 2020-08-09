using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// The core of the whole game.
/// 
/// <para>
/// The GameGrid's purpose is to contain information about all tiles "acknowledged" by the script
/// and to provide methods which work on this collection of tiles.
/// </para>
/// 
/// <para>
/// Most GameGrid components and scripts related to tiles use
/// the methods inside this class to get information about other tiles.
/// These methods only work on tiles contained inside the internal list of all tiles.
/// </para>
/// 
/// <para>
/// Being "acknowledged" (contained inside the tile list of this script) is crucial as many
/// important scripts (IE: Triggers) rely heavily on the methods inside this script. The user should make
/// his priority to akcnowledge a tile after it is ready. Some scripts already provide this functionality
/// automatically, so the user doesn't have to worry about acknowledging the GridObject manually.
/// </para>
/// 
/// <para>
/// Calling methods on this script is done through the singleton instance, as there can exist only one GameGrid
/// at a time.
/// </para>
/// 
/// </summary>
public class GameGrid : MonoBehaviour
{
    /// <summary>
    /// The amount of unity's X axis units a single tile will occupy.
    /// </summary>
    [SerializeField]
    private float gridX = 0f;

    /// <summary>
    /// See: <see cref="gridX"/>
    /// </summary>
    public float GridX => gridX;

    /// <summary>
    /// The amount of unity's Y axis units a single tile will occupy.
    /// </summary>
    [SerializeField]
    private float gridY = 0f;

    /// <summary>
    /// See: <see cref="gridY"/>
    /// </summary>
    public float GridY => gridY;


    /// <summary>
    /// The static instance of this singleton. An interface for the user to call methods on this script.
    /// There can exist only one instance at a time.
    /// </summary>
    private static GameGrid instance;


    /// <summary>
    /// See: <see cref="instance"/>
    /// </summary>
    public static GameGrid INSTANCE => instance;

    /// <summary>
    /// A list containing all GridObjects that should be handled in game.
    /// All ready GridObjects should be contained inside this list. A large amount of methods inside this class
    /// rely that the GridObjects have a reference stored inside this list.
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
    /// <para>The user shouldn't have the ability to access this list directly. He should use encapsulation methods.
    /// IE: (<see cref="AddEntry(GridObject)"/>)</para>
    /// 
    /// 
    /// </summary>
    private LinkedList<GridObject> gridObjects = new LinkedList<GridObject>();


    private List<GridObject> rowRandomSpawners = new List<GridObject>();

    [SerializeField]
    private GameObject spawnObject = null;

    /// <summary>
    /// Adds a GridObject entry inside the GameGrid system, which enables such object to be acknowledged by the system and the GameGrid methods.
    /// </summary>
    /// <param name="gridObject"></param>
    public void AddEntry(GridObject gridObject)
    {
        gridObjects.AddLast(gridObject);
        NotifySurroundingsObservers(gridObject);
    }

    /// <summary>
    /// Returns an array of all GridObjects acknowledged by the game.
    /// </summary>
    /// <returns></returns>
    public GridObject[] GetAllGridObjects()
    {
        return gridObjects.ToArray();
    }

    /// <summary>
    /// Triggers all GridObjectTriggers on the position of the gridObject in the parameter that are not the parameter itself.
    /// </summary>
    /// <param name="gridObject"></param>
    public void NotifyTriggers(GridObject gridObject)
    {
        GridObject[] objects = GridObjectsAtObjectFiltered(gridObject);
        for (int i = 0; i < objects.Length; i++)
        {
            GridObject objectAt = objects[i];
            // Check if the object wasn't destroyed after we've gathered the objects.
            if (objectAt == null)
            {
                Debug.LogWarning("NotifyTriggers caught an edge case: GridObjectsAtObjectFiltered contained a null reference, it's possible that the object got destroyed in the meantime.");
                RemoveEntry(objectAt);
                continue;
            }

            GridObjectTrigger trigger;
            trigger = objectAt.GetComponent<GridObjectTrigger>();

            if(trigger != null)
            {
                trigger.NotifyTrigger(gridObject);
            }


        }

    }

    /// <summary>
    /// Calls SurroundingsChanged on all observers in a square relative to the GridObject in the parameter.
    /// 
    /// When a GridObject is acknowledged, all surrounding tiles that have a SurroundingsObserver component get their SurroundingsChanged
    /// method called.
    /// 
    /// </summary>
    /// <param name="gridObject"></param>
    public void NotifySurroundingsObservers(GridObject gridObject)
    {
        GridObject[] surroundingObjects = GridObjectsAtRelativeSquare(gridObject);

        for (int i = 0; i < surroundingObjects.Length; ++i)
        {
            GridObject surroundingObject = surroundingObjects[i];
            if (surroundingObject == null)
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
    /// This method checks if two tiles of the mase TileType would happen to exist on the same tile.
    /// This check is called when a GridObject validates its position - if the check would detect the latter
    /// scenario, one of the two objects would be destroyed. The destruction is performed in the benefit of the
    /// calling object.
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

        for (int i = 0; i < gridObjectsAtPosition.Length; i++)
        {
            GridObject gridObjectAtPosition = gridObjectsAtPosition[i];
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
    /// Returns an array of gridObjects at the grid position X and Y taken from the parameters.
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

    /// <summary>
    /// Private method to standardly initialize the singleton of this script.
    /// </summary>
    private void StandartSingletonInitialization()
    {
        if (INSTANCE != null)
        {
            Destroy(gameObject);
            Debug.LogWarning($"Tried to create a second GameGrid script. Destroying game object {gameObject}.");
        }
        instance = this;
    }

    /// <summary>
    /// Returns an array of GridObjects from the position X, Y relative to the position of the GridObject in the parameter.
    /// </summary>
    /// <returns></returns>
    public GridObject[] ObjectsAtRelative(GridObject gridObject, int X, int Y)
    {
        return ObjectsAt(gridObject.X + X, gridObject.Y + Y);
    }

    /// <summary>
    /// Returns an array of GridObjects by walking from the position of the GridObject in the parameter by walking 1 tile in the direction
    /// in the parameter.
    /// </summary>
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

    /// <summary>
    /// Somewhat deprecated. Adds entries to all GridObjects queued inside the GameGridUtilities class. This is done when
    /// there exist GridObjects instances inside the scene before this singleton was initialized.
    /// 
    /// This is deprecated since most GridObjects entries are added when they are spawned from a structure or otherwise.
    /// This method was used during testing when there wasn't structure spawning in place.
    /// 
    /// </summary>
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
