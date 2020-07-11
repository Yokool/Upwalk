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


    private LinkedList<GridObject> gridObjects = new LinkedList<GridObject>();


    [SerializeField]
    private GameObject spawnObject = null;


    public void AddEntry(GridObject gridObject)
    {

        ValidationData validationData = CheckForValidation(gridObject);

        if (!validationData.isValid)
        {
            foreach(GridObject overlappingObject in validationData.objectsAtPosition)
            {
                Destroy(overlappingObject);
            }
        }

        gridObjects.AddLast(gridObject);
    }


    public ValidationData CheckForValidation(GridObject gridObject)
    {

        TileTypeData data = TileTypeDataDatabase.TileTypeDatabase[gridObject.m_TileType];

        if (data.canExistMultiple)
        {
            return new ValidationData(null, true);
        }


        GridObject[] gridObjectsAtPosition = ObjectsAt(gridObject);
        
        foreach(GridObject gridObjectAtPosition in gridObjectsAtPosition)
        {

            if(gridObjectAtPosition.m_TileType == gridObject.m_TileType)
            {
                return new ValidationData(gridObjectsAtPosition, false);
            }

        }

        return new ValidationData(null, true);

    }

    public void RemoveEntry(GridObject gridObject)
    {
        gridObjects.Remove(gridObject);
    }

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

    public GridObject[] ObjectsAt(GridObject gridObject)
    {
        return ObjectsAt(gridObject.X, gridObject.Y);
    }

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
        if(INSTANCE != null)
        {
            Destroy(gameObject);
            Debug.LogWarning($"Tried to create a second GameGrid script. Destroying game object {gameObject}.");
        }
        instance = this;


        BuildSpawn();

    }


}

public struct ValidationData
{

    public GridObject[] objectsAtPosition;
    public bool isValid;

    public ValidationData(GridObject[] objectsAtPosition, bool isValid)
    {
        this.objectsAtPosition = objectsAtPosition;
        this.isValid = isValid;
    }

}


