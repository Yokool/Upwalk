using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWeightedObjectProvider : MonoBehaviour, IWeightedObjectProvider<GameObject>
{
    [SerializeField]
    private GameObject wall;

    public WeightObjectTie<GameObject>[] GetWeightedObjects()
    {
        return new WeightObjectTie<GameObject>[]
        {
            new WeightObjectTie<GameObject>()
            {
                weightedObject = wall,
                weight = 1
            }
        };
    }

}
