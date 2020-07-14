using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
public class GridObjectSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject objToSpawn;

    private GridObject gridObject;

    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();
    }

    public void SpawnObject()
    {
        GameObject instance = Instantiate(objToSpawn);


        GridObject instanceGridObject = instance.GetComponent<GridObject>();
        if (instanceGridObject == null)
        {
            Debug.LogError($"GridObjectSpawner: Prefab/GameObject - {objToSpawn} - contained in field inside {gameObject} does not have a {nameof(GridObject)} component attached.");
            return;
        }

        instanceGridObject.SetPosition(gridObject.X, gridObject.Y);


    }


}
