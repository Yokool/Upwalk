using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeightedObjectProvider : MonoBehaviour, IWeightedObjectProvider<GameObject>
{
    [SerializeField]
    private GameObject monk_Easy = null;
    [SerializeField]
    private GameObject monk_Medium = null;
    [SerializeField]
    private GameObject monk_Hard = null;

    // DIAMOND GOLEM
    [SerializeField]
    private GameObject diamondGolem_Easy = null;
    [SerializeField]
    private GameObject diamondGolem_Medium = null;
    [SerializeField]
    private GameObject diamondGolem_Hard = null;

    // EYE
    [SerializeField]
    private GameObject eye_Easy = null;
    [SerializeField]
    private GameObject eye_Medium = null;
    [SerializeField]
    private GameObject eye_Hard = null;

    [SerializeField]
    private GameObject slug = null;
    [SerializeField]
    private GameObject tower = null;

    public WeightObjectTie<GameObject>[] GetWeightedObjects()
    {

        return new WeightObjectTie<GameObject>[]
        {
            new WeightObjectTie<GameObject>()
            {
                weightedObject = null,
                weight = 250
            },
            new WeightObjectTie<GameObject>()
            {
                weightedObject = monk_Easy,
                weight = 3
            },
            new WeightObjectTie<GameObject>()
            {
                weightedObject = monk_Medium,
                weight = 2
            },
            new WeightObjectTie<GameObject>()
            {
                weightedObject = monk_Hard,
                weight = 1
            },
            new WeightObjectTie<GameObject>()
            {
                weightedObject = diamondGolem_Easy,
                weight = 3
            },
            new WeightObjectTie<GameObject>()
            {
                weightedObject = diamondGolem_Medium,
                weight = 2
            },
            new WeightObjectTie<GameObject>()
            {
                weightedObject = diamondGolem_Hard,
                weight = 1
            },
            new WeightObjectTie<GameObject>()
            {
                weightedObject = eye_Easy,
                weight = 1
            },
            new WeightObjectTie<GameObject>()
            {
                weightedObject = eye_Medium,
                weight = 1
            },
            new WeightObjectTie<GameObject>()
            {
                weightedObject = eye_Hard,
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
