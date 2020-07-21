using System.Collections.Generic;
using UnityEngine;

public static class RandomWeightUtility
{
    
    public static T Pick<T>(WeightObjectTie<T>[] pickFrom) where T : class
    {

        int totalWeight = 0;


        foreach (WeightObjectTie<T> weightObject in pickFrom)
        {
            totalWeight += weightObject.weight;
        }

        int generatedNumber = Random.Range(0, totalWeight);

        T pickedObject = null;

        List<T> list = new List<T>();

        for (int i = 0; i < pickFrom.Length; ++i)
        {
            WeightObjectTie<T> objectTie = pickFrom[i];

            T weightedObject = objectTie.weightedObject;

            for (int j = 1; j <= objectTie.weight; ++j)
            {
                list.Add(weightedObject);
            }

        }


        pickedObject = list[generatedNumber];

        return pickedObject;
    }

}
