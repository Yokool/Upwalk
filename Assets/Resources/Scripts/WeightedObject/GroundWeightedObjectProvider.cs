using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundWeightedObjectProvider : MonoBehaviour, IWeightedObjectProvider<GameObject>
{
    [SerializeField]
    private GameObject groundPrefab;

    public WeightObjectTie<GameObject>[] GetWeightedObjects()
    {
        return new WeightObjectTie<GameObject>[]
        {
            new WeightObjectTie<GameObject>()
            {
                weightedObject = groundPrefab,
                weight = 1
            }

        };
    }
}
