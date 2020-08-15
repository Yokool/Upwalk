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

    private IWeightedObjectProvider<GameObject>[] randomObjectLists;

    private GridObject gridObject;

    private GridStructureBehaviour gridStructureBehaviour;
    
    private void OnDestroy()
    {
        GameGrid.INSTANCE.RemoveRandomRowEntry(gridObject);
    }
    
    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();
        spawner = GetComponent<GridObjectSpawner>();
        randomObjectLists = GetComponents<IWeightedObjectProvider<GameObject>>();
        gridStructureBehaviour = GetComponent<GridStructureBehaviour>();
        GameGrid.INSTANCE.AddRandomRowEntry(gridObject);
    }

    public void SpawnRandomObjects()
    {

        for (int i = 0; i < randomObjectLists.Length; i++)
        {
            IWeightedObjectProvider<GameObject> weightedObjectProvider = randomObjectLists[i];
            // Prefab reference
            GameObject objectToSpawn = RandomWeightUtility.Pick<GameObject>(weightedObjectProvider.GetWeightedObjects());

            // Weighted objects can contain a null reference to indicate that nothing should be spawned.
            if(objectToSpawn == null)
            {
                continue;
            }

            // Stored in a field
            spawner.ObjToSpawn = objectToSpawn;

            // Prefab => Instance
            spawner.SpawnObject();

            // Reset the field
            spawner.ObjToSpawn = null;

        }
        // Build this tile 1 step north
        gridStructureBehaviour.BuildItself();


    }

    [ContextMenu("TestSpawningRandomObject")]
    public void TestSpawning()
    {
        SpawnRandomObjects();
    }

}
