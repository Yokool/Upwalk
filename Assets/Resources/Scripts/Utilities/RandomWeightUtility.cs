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


        int currentWeight = 0;

        T pickedObject = null;

        for(int i = 0; i < pickFrom.Length; ++i)
        {
            WeightObjectTie<T> tie = pickFrom[i];
            currentWeight += tie.weight;

            if(generatedNumber <= currentWeight)
            {
                pickedObject = tie.weightedObject;
                break;
            }

        }

        return pickedObject;

    }

}
