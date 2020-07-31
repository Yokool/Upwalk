using UnityEngine;

[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(Moveable))]
public class TileIterator : MonoBehaviour
{
    [SerializeField]
    public IteratorStruct[] iteratorStructs;

    private GridObject gridObject;
    private Moveable moveable;

    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();
        moveable = GetComponent<Moveable>();
    }

    public void MoveTilesOnIteration(int currentIteration)
    {
        foreach(IteratorStruct iteratorStruct in iteratorStructs)
        {
            if(iteratorStruct.interationTurn == currentIteration)
            {
                GridObject gridObject = gameObject.GetComponent<GridObject>();

                if(gridObject == null)
                {
                    Debug.LogError($"Class: {nameof(TileIterator)} - method: {nameof(MoveTilesOnIteration)} - gameObject: {gameObject}: does not have a {nameof(GridObject)} component which is needed for the method to run.");
                }

                moveable.MoveObject_Relative(iteratorStruct.X, iteratorStruct.Y);

            }
        }
    }

}
