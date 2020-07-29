using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(GridObjectSpawnerRandomExtension))]
public class RandomSpawnerAcknowledger : MonoBehaviour
{

    private void Awake()
    {
        GameGrid.INSTANCE.AddRandomRowEntry(GetComponent<GridObject>());
    }

}
