using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObjectSpawner))]
public class GridObjectSpawnerRandomExtension : MonoBehaviour
{

    private GridObjectSpawner spawner;

    private GameObject[] randomObjectList;

    private void OnEnable()
    {
        spawner = GetComponent<GridObjectSpawner>();
    }

    public void SpawnRandomObject()
    {

    }

}
