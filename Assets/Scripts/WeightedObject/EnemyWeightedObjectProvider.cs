using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeightedObjectProvider : MonoBehaviour, IWeightedObjectProvider<GameObject>
{
    [SerializeField]
    private GameObject monk;
    [SerializeField]
    private GameObject diamondGolem;
    [SerializeField]
    private GameObject eye;
    [SerializeField]
    private GameObject slug;
    [SerializeField]
    private GameObject tower;

    public WeightObjectTie<GameObject>[] GetWeightedObjects()
    {

        return new WeightObjectTie<GameObject>[]
        {
            new WeightObjectTie<GameObject>()
            {
                weightedObject = null,
                weight = 125
            },
            new WeightObjectTie<GameObject>()
            {
                weightedObject = monk,
                weight = 1
            },
            new WeightObjectTie<GameObject>()
            {
                weightedObject = diamondGolem,
                weight = 1
            },
            new WeightObjectTie<GameObject>()
            {
                weightedObject = eye,
                weight = 1
            },
            new WeightObjectTie<GameObject>()
            {
                weightedObject = slug,
                weight = 1
            },
            new WeightObjectTie<GameObject>()
            {
                weightedObject = tower,
                weight = 1
            }
        };

    }

}
