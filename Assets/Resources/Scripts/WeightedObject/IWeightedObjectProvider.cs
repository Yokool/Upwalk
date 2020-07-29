public interface IWeightedObjectProvider<T>
{
    WeightObjectTie<T>[] GetWeightedObjects();
}