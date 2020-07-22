using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(GridObjectSpawner))]
[RequireComponent(typeof(IWeightedObjectProvider<GameObject>))]
[RequireComponent(typeof(GridStructureBehaviour))]
public class GridObjectSpawnerRandomExtension : MonoBehaviour
{

    private GridObjectSpawner spawner;

    private IWeightedObjectProvider<GameObject> randomObjectList;

    private GridObject gridObject;

    private GridStructureBehaviour gridStructureBehaviour;

    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();
        spawner = GetComponent<GridObjectSpawner>();
        randomObjectList = GetComponent<IWeightedObjectProvider<GameObject>>();
        gridStructureBehaviour = GetComponent<GridStructureBehaviour>();
    }

    public void SpawnRandomObject()
    {
        // Prefab reference
        GameObject objectToSpawn = RandomWeightUtility.Pick<GameObject>(randomObjectList.GetWeightedObjects());

        // Stored in a field
        spawner.ObjToSpawn = objectToSpawn;

        // Prefab => Instance
        spawner.SpawnObject();

        // Reset the field
        spawner.ObjToSpawn = null;

        // Build this tile 1 step north
        gridStructureBehaviour.BuildItself();

    }

    [ContextMenu("TestSpawningRandomObject")]
    public void TestSpawning()
    {
        SpawnRandomObject();
    }

}
