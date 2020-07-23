using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
public class GridObjectSpawner : MonoBehaviour
{

    [SerializeField]
    public GameObject ObjToSpawn { get; set; }

    private GridObject gridObject;

    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();
    }

    public GameObject SpawnObject()
    {
        GameObject instance = Instantiate(ObjToSpawn);
        

        GridObject instanceGridObject = instance.GetComponent<GridObject>();
        if (instanceGridObject == null)
        {
            Debug.LogError($"GridObjectSpawner: Prefab/GameObject - {ObjToSpawn} - contained in field inside {gameObject} does not have a {nameof(GridObject)} component attached.");
            return null;
        }

        instanceGridObject.SetGridPosition(gridObject.X, gridObject.Y);
        instanceGridObject.Establish();

        return instance;

    }


}
